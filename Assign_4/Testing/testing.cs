//-----------------------------------------------------------------------
// <copyright file="Testing.cs" company="LakeheadU">
//     Copyright ENGI-3675. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Testing
{
    using System.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Assign_4.App_Code.ServerConn;
    using OpenQA.Selenium.Firefox;

    /// <summary>
    /// ENGI3675 Test cases for the requirements of Assignment 4
    /// </summary>
    [TestClass]
    public class Testing
    {
        /// <summary>
        /// SQL injection attempted on the safe (prepared statements) and unsafe insertion pages
        /// Security is ensured by testing for the script not being run
        /// Injection is then confirmed by ensuring all students have a gpa of 4.0
        /// Additionally: test confirms that the new entry used to piggy back off of is removed
        /// </summary>
        [TestMethod]
        public void injectionAttack()
        {
            FirefoxDriver ffox = new FirefoxDriver();

            ffox.Navigate().GoToUrl("localhost:58374/safeinsert.aspx");
            ffox.FindElementById("sname").SendKeys("Bart Simpson',4);delete from students where name='Bart Simpson';update students set gpa = 4;--");
            ffox.FindElementById("sgpa").SendKeys("2.0");
            ffox.FindElementById("InsertStudent").Click();

            DataTable results = ServerConn.MyQuery("SELECT * FROM students");

            bool InjectionFailed = false;

            foreach (DataRow d in results.Rows)
            {
                if (d.ItemArray[1].Equals("Bart Simpson',4);delete from students where name='Bart Simpson';update students set gpa = 4;--"))
                    InjectionFailed = true;
            }

            Assert.IsTrue(InjectionFailed, "Error: Safe insert did not prevent the SQL injection");

            ffox.Navigate().GoToUrl("localhost:58374/unsafeinsert.aspx");
            ffox.FindElementById("sname").SendKeys("Bart Simpson',4);delete from students where name='Bart Simpson';update students set gpa = 4;--");
            ffox.FindElementById("sgpa").SendKeys("4.0");
            ffox.FindElementById("InsertStudent").Click();


            results = ServerConn.MyQuery("SELECT * FROM students");
            foreach (DataRow d in results.Rows)
            {
                Assert.AreEqual((float)4, (float)d.ItemArray[2], "Error: Student {0} does not have a GPA of 4", d.ItemArray[1].ToString());
                Assert.AreNotEqual("Bart Simpson", d.ItemArray[1].ToString(), "Error you didn't cover your tracks!");
            }
        }

        /// <summary>
        /// XSS Attack Test case:
        /// Injection of javascript that changes the background image
        /// The results from the database are tested to ensure the script is escaped
        /// </summary>
        [TestMethod]
        public void XSSAttack()
        {
            FirefoxDriver ffox = new FirefoxDriver();

            ffox.Navigate().GoToUrl("localhost:58374/safeinsert.aspx");
            ffox.FindElementById("sname").SendKeys("23',3);<script>document.body.setAttribute('style','background-image: url(\"http://vignette1.wikia.nocookie.net/simpsons/images/7/7b/Eat_My_Shorts.jpg/revision/latest?cb=20100606181712\");');</script>--");
            ffox.FindElementById("sgpa").SendKeys("2.0");
            ffox.FindElementById("InsertStudent").Click();

            ffox.Navigate().GoToUrl("localhost:58374/unsafeshowall.aspx");
            Assert.IsTrue(ffox.FindElementByTagName("body").GetAttribute("style").Contains("url(\"http://vignette1.wikia.nocookie.net/simpsons/images/7/7b/Eat_My_Shorts.jpg/revision/latest?cb=20100606181712"), "XSS Script failed to change background image");

            ffox.Navigate().GoToUrl("localhost:58374/safeshowall.aspx");
            Assert.IsFalse(ffox.FindElementByTagName("body").GetAttribute("style").Contains("url(\"http://vignette1.wikia.nocookie.net/simpsons/images/7/7b/Eat_My_Shorts.jpg/revision/latest?cb=20100606181712"), "SafeShowAll page failed in preventing background image from being injected");
        }
    }
}
