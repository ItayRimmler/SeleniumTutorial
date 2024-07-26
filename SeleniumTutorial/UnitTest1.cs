using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace SeleniumTutorial
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // Defining driver for our browser...
            IWebDriver driver = new ChromeDriver();

            // Entering a random song name generator...
            driver.Navigate().GoToUrl("https://www.chosic.com/song-name-generator-by-genre-and-mood/#google_vignette");

            // Maximazing the window...
            driver.Manage().Window.Maximize();

            // Entering to Youtube...
            /////////////////    
            driver.SwitchTo().NewWindow(WindowType.Tab);
            driver.Navigate().GoToUrl("https://www.youtube.com");
                
            // Finding search bar in Youtube...
            IWebElement bar = driver.FindElement(By.Name("search_query"));


            // Getting window handles...
            string[] handles = { driver.WindowHandles[0], driver.WindowHandles[1] };

            // Going back to the song generator...
            driver.SwitchTo().Window(handles[0]);

            // Generating a songs...
            /////////////////    
            // Finding the buttons...
            IWebElement gen = driver.FindElement(By.ClassName("go-top"));
            IWebElement select1 = driver.FindElement(By.Name("generator-moods"));
            IWebElement select2 = driver.FindElement(By.Name("generator-genre"));
            SelectElement mood = new SelectElement(select1);
            SelectElement genre = new SelectElement(select2);

            // Getting two random integers, between 0-6 and 0-10...
            Random rnd = new Random();
            int ifeel = rnd.Next(0, 7);
            int iwant = rnd.Next(0, 11);

            // Selecting a random mood and a random genre...
            mood.SelectByIndex(ifeel);
            genre.SelectByIndex(iwant);
            
            // Generating...
            gen.Click();
            
            // I don't understand that part in the code tbh...
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(200));

            // Locate all elements with the same class
            IWebElement spotify_button = wait.Until(d => d.FindElement(By.CssSelector("a.search-spotify")));

            /////////////////   

            // Copying and pasting name...
            spotify_button.Click();
            IWebElement link = wait.Until(d => d.FindElement(By.ClassName("sp-pl-link")));
            link.Click();
            string tosong = driver.WindowHandles[2];
            driver.SwitchTo().Window(tosong);
            IWebElement name_container = driver.FindElement(By.CssSelector("h1 .suggest-song-name"));
            string name = name_container.Text;
            driver.SwitchTo().Window(handles[1]);
            bar.Clear();
            bar.SendKeys(name);

            // Searching the song...
            IWebElement search = driver.FindElement(By.Id("search-icon-legacy"));
            search.Click();

            // Entering to the video...
            IWebElement video = wait.Until(d => d.FindElement(By.Id("video-title")));
            video.Click();

        }
    }
}

