using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;

class MarketPriceSearch
{

    private static string ProductName { get; set; }
    private static string ProductDescription { get; set; }
    private static string ProductPrice { get; set; }

    private static ChromeDriver InitializeWebDriver()
    {
        string chromeDriverPath = @"chromedriver2.exe";
        ChromeDriverService service = ChromeDriverService.CreateDefaultService(chromeDriverPath);
        ChromeOptions options = new ChromeOptions();
        options.AddArguments("headless");   // Browseri Gizleme -> eğer gizlenirse a101'de ürünlere erişememe problemi oluyor.
        service.HideCommandPromptWindow = true;
        return new ChromeDriver(service, options);
    }

    public static async Task ChangeNowQuery()
    {
        try
        {
            using (var driver = InitializeWebDriver())
            {
                driver.Manage().Window.Maximize();
                await sokmarket(driver);
                await a101market(driver);
                await onurmarket(driver);
                await carrefoursamarket(driver);
            }
        }
        catch
        {
            Console.WriteLine("Bir Hata Oluştu.");
        }
    }

    private static async Task sokmarket(ChromeDriver driver)
    {
        try
        {
            driver.Url = "https://www.sokmarket.com.tr/";
            await Task.Delay(2000);

            IWebElement searchinput = driver.FindElement(By.XPath("/html/body/div[2]/header/div[2]/div/div[2]/div/div/div[1]/input"));
            searchinput.SendKeys(ProductName);

            searchinput.SendKeys(Keys.Enter);

            await Task.Delay(2000);
            
            ProductDescription = driver.FindElement(By.XPath("/html/body/div[2]/div/main/div[2]/div/div[2]/div[1]/div[1]/div/a/div/div[2]/div/div[2]/h2")).Text;
            ProductPrice = driver.FindElement(By.XPath("/html/body/div[2]/div/main/div[2]/div/div[2]/div[1]/div[1]/div/a/div/div[2]/div/div[2]/div/div/span")).Text;
            Console.WriteLine(" ######### ÜRÜN FİYATLARI ######### ");
            Console.WriteLine("Şok Market => " + ProductDescription + " = " + ProductPrice);
        }
        catch
        {
            Console.WriteLine("Şok Market => Bir Hata Oluştu.");
        }
    }

    private static async Task a101market(ChromeDriver driver)
    {
        try
        {
            driver.Url = "https://www.a101.com.tr/kapida";
            await Task.Delay(2000);

            try
            {
                IWebElement notify = driver.FindElement(By.XPath("//*[@id=\"CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll\"]"));
                notify.Click();

                IWebElement searchinput = driver.FindElement(By.XPath("//*[@id=\"searchbar\"]"));
                searchinput.SendKeys(ProductName);

                searchinput.SendKeys(Keys.Enter);

                await Task.Delay(2000);

                ProductDescription = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[3]/div/div/div/div/div[1]/div[2]/div[2]/div[1]/div/div/div/div[3]/div[1]")).Text;
                ProductPrice = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[3]/div/div/div/div/div[1]/div[2]/div[2]/div[1]/div/div/div/div[3]/div[2]/div[2]")).Text;
                Console.WriteLine("A101 Market => " + ProductDescription + " = " + ProductPrice);
            }
            catch
            {
                IWebElement searchinput = driver.FindElement(By.XPath("//*[@id=\"searchbar\"]"));
                searchinput.SendKeys(ProductName);

                searchinput.SendKeys(Keys.Enter);

                await Task.Delay(2000);

                ProductDescription = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[3]/div/div/div/div/div[1]/div[2]/div[2]/div[1]/div/div/div/div[3]/div[1]")).Text;
                ProductPrice = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[3]/div/div/div/div/div[1]/div[2]/div[2]/div[1]/div/div/div/div[3]/div[2]/div[2]")).Text;
                Console.WriteLine("A101 Market => " + ProductDescription + " = " + ProductPrice);

            }
        }
        catch
        {
            Console.WriteLine("A101 Market => Bir Hata Oluştu.");
        }
    }

    private static async Task onurmarket(ChromeDriver driver)
    {
        try
        {
            driver.Url = "https://www.onurmarket.com/Arama";
            await Task.Delay(2000);

            IWebElement searchinput = driver.FindElement(By.XPath("//*[@id=\"txtbxArama\"]"));
            searchinput.SendKeys(ProductName);

            searchinput.SendKeys(Keys.Enter);

            await Task.Delay(2000);

            ProductDescription = driver.FindElement(By.XPath("//*[@id=\"ProductPageProductList\"]/div/div/div[2]/div[2]/a")).Text;
            ProductPrice = driver.FindElement(By.XPath("//*[@id=\"ProductPageProductList\"]/div/div/div[2]/div[3]/div/span[1]")).Text;
            Console.WriteLine("Onur Market => " + ProductDescription + " = " + ProductPrice);
        }
        catch
        {
            Console.WriteLine("Onur Market => Bir Hata Oluştu.");
        }
    }

    private static async Task carrefoursamarket(ChromeDriver driver)
    {
        try
        {
            driver.Url = "https://www.carrefoursa.com/";
            await Task.Delay(2000);

            IWebElement searchinput = driver.FindElement(By.XPath("//*[@id=\"js-site-search-input\"]"));
            searchinput.SendKeys(ProductName);

            searchinput.SendKeys(Keys.Enter);

            Task.Delay(2000);

            ProductDescription = driver.FindElement(By.XPath("//*[@id=\"30468950\"]/a/span[2]")).Text;
            ProductPrice = driver.FindElement(By.XPath("//*[@id=\"30468950\"]/a/span[5]/div/span[3]")).Text;
            Console.WriteLine("Carrefoursa Market => " + ProductDescription + " = " + ProductPrice);
            Console.WriteLine("######### ÜRÜN FİYATLARI ######### ");
        }
        catch
        {
            Console.WriteLine("Carrefoursa Market => Bir Hata Oluştu.");
        }
    }



    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        while (true)
        {
            ConsoleColor foreColor = ConsoleColor.White;
            Console.ForegroundColor = foreColor;
            Console.WriteLine("Fiyatını Görmek İstediğiniz Ürünü Yazın.");
            ProductName = Console.ReadLine();
            Console.Clear();
            ConsoleColor newForeColor = ConsoleColor.Red;
            Console.ForegroundColor = newForeColor;

            await Task.Run(async () =>
            {
                await ChangeNowQuery();
            });
        }
    }
}