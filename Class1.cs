using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace seleniun_prj;

public class Seleniunprj
{
    [Test]
    public void LoginPinterest_Exitoso()
    {
        IWebDriver driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://www.pinterest.com/login/");

        driver.FindElement(By.Name("id")).SendKeys("paulices30@gmail.com");
        driver.FindElement(By.Name("password")).SendKeys("rastacuando-1");
        driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-test-id='homefeed']")));

        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(driver.Url.Contains("pinterest.com"));
        driver.Quit();
    }

    [Test]
    public void CrearPinPinterest()
    {
        IWebDriver driver = new ChromeDriver();
        driver.Manage().Window.Maximize();

        // Login
        driver.Navigate().GoToUrl("https://www.pinterest.com/login/");
        driver.FindElement(By.Name("id")).SendKeys("paulices30@gmail.com");
        driver.FindElement(By.Name("password")).SendKeys("rastacuando-1");
        driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-test-id='homefeed']")));

        driver.Navigate().GoToUrl("https://www.pinterest.com/pin-creation-tool/");
        IWebElement uploadInput = driver.FindElement(By.CssSelector("input[type='file']"));
        uploadInput.SendKeys(@"C:\Users\pauli\Pictures\image.jpg");
        driver.FindElement(By.CssSelector("textarea[placeholder='Añadir título']")).SendKeys("Pin de prueba");
        driver.FindElement(By.CssSelector("button[aria-label='Guardar']")).Click();
        driver.Quit();
    }

    [Test]
    public void EditarPinPinterest()
    {
        IWebDriver driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

        try
        {
            // 1. Login (puedes separar este paso en otro método)
            driver.Navigate().GoToUrl("https://www.pinterest.com/login/");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("id")));
            driver.FindElement(By.Name("id")).SendKeys("paulices30@gmail.com");
            driver.FindElement(By.Name("password")).SendKeys("rastacuando-1");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-test-id='homefeed']")));

            driver.Navigate().GoToUrl("https://www.pinterest.com/paulices/_created/");

            var pin = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div[data-test-id='pin']")));
            pin.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-test-id='closeup-modal']")));

            var editButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[aria-label='Editar pin']")));
            editButton.Click();

            var titleInput = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("textarea[placeholder='Añadir título']")));
            titleInput.Clear();
            titleInput.SendKeys("Título modificado por automatización");

            var saveBtn = driver.FindElement(By.CssSelector("button[data-test-id='closeup-save-button']"));
            wait.Until(d => saveBtn.Enabled && saveBtn.Displayed);
            saveBtn.Click();

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div[data-test-id='closeup-modal']")));
        }
        catch (Exception ex)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Error editando pin: " + ex.Message);
        }
        finally
        {
            driver.Quit();
        }
    }

    [Test]
    public void EliminarPinPinterest()
    {
        IWebDriver driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

        try
        {
            // 1. Login
            driver.Navigate().GoToUrl("https://www.pinterest.com/login/");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("id")));
            driver.FindElement(By.Name("id")).SendKeys("paulices30@gmail.com");
            driver.FindElement(By.Name("password")).SendKeys("rastacuando-1");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-test-id='homefeed']")));
            driver.Navigate().GoToUrl("https://www.pinterest.com/paulices/_created/");
            var pin = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div[data-test-id='pin']")));
            pin.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-test-id='closeup-modal']")));
            var menuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[aria-label='Más opciones']")));
            menuBtn.Click();
            var deleteBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Eliminar')]")));
            deleteBtn.Click();
            var confirmBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Eliminar')]")));
            confirmBtn.Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div[data-test-id='closeup-modal']")));

        }
        catch (Exception ex)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Error eliminando pin: " + ex.Message);
        }
        finally
        {
            driver.Quit();
        }
    }
}
