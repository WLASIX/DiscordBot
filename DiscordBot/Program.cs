using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        public static string[] login = new string[] { };
        public static string[] password = new string[] { };
        public static IWebDriver driver;
        public static IWebElement element;

        public static void Run()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://discord.com/channels/916546904077262878/916546904077262881");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            for (int j = 0; j < login.Length - 1; ++j)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.open('https://discord.com/channels/916546904077262878/916546904077262881');");
            }

            for (int i = 0; i < login.Length; ++i)
            {
                // логин
                element = driver.FindElement(By.XPath("//*[@id='app-mount']/div[2]/div/div/div/div/form/div/div/div[1]/div[2]/div[1]/div/div[2]/input"));
                element.SendKeys(login[i]);

                // пароль
                element = driver.FindElement(By.XPath("//*[@id='app-mount']/div[2]/div/div/div/div/form/div/div/div[1]/div[2]/div[2]/div/input"));
                element.SendKeys(password[i]);

                // войти
                driver.FindElement(By.XPath("//*[@id='app-mount']/div[2]/div/div/div/div/form/div/div/div[1]/div[2]/button[2]")).Click();

                Thread.Sleep(1488);

                if (i < login.Length - 1)
                    driver.SwitchTo().Window(driver.WindowHandles[i + 1]);
            }

            for (int j = 0; j < 2; ++j)
                for (int i = 0; i < login.Length; ++i)
                {
                    driver.SwitchTo().Window(driver.WindowHandles[i]);
                    SendMessage();
                }
        }

        public static void SendMessage()
        {
            while (true)
            {
                element = driver.FindElement(By.CssSelector("#app-mount > div.app-1q1i1E > div > div.layers-3iHuyZ.layers-3q14ss > div > div > div > div.content-98HsJk > div.chat-3bRxxu > div.content-yTz4x3 > main > form > div > div > div > div.scrollableContainer-2NUZem.webkit-HjD9Er > div > div.textArea-12jD-V.textAreaSlate-1ZzRVj.slateContainer-3Qkn2x > div.markup-2BOw-j.slateTextArea-1Mkdgw.fontSize16Padding-3Wk7zP > div"));
                element.SendKeys(GetRandomText());
                element.SendKeys(Keys.Enter);
            }
        }

        static string GetRandomText()
        {
            Random rand = new Random();
            string[] text = { "spam", "spaam", "spaaam" };
            return text[rand.Next(3)];
        }
    }
}
