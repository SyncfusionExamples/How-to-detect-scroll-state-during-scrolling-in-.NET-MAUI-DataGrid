using Syncfusion.Maui.Themes;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;

namespace DataGridSample
{
   

    public partial class MainPage : ContentPage
    {


        private double lastScrollOffset = 0;
        private DateTime lastScrollTime = DateTime.Now;
        private const double flingThreshold = 3;
        private const int idleTimeout = 200;

        private System.Timers.Timer idleTimer;
        public MainPage()
        {

            InitializeComponent();

           var visualContainer =  dataGrid.GetVisualContainer();

            if (visualContainer != null && visualContainer.ScrollOwner != null)
            {
                visualContainer.ScrollOwner.Scrolled += ScrollOwner_Scrolled;
            }


            idleTimer = new System.Timers.Timer(idleTimeout);
            idleTimer.Elapsed += (s, e) =>
            {
                idleTimer.Stop();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Debug.WriteLine("Idle detected");
                });
            };

        }



        private void ScrollOwner_Scrolled(object? sender, ScrolledEventArgs e)
        {
            var currentOffset = e.ScrollY;
            var currentTime = DateTime.Now;

            var deltaOffset = currentOffset - lastScrollOffset;
            var deltaTime = (currentTime - lastScrollTime).TotalMilliseconds;

            if (deltaTime > 0)
            {
                var velocity = deltaOffset / deltaTime;

                if (Math.Abs(velocity) > flingThreshold)
                {
                    Debug.WriteLine("Fling detected");
                }
                else
                {
                    Debug.WriteLine("Drag detected");
                }
            }

            idleTimer.Stop();
            idleTimer.Start();

            lastScrollOffset = currentOffset;
            lastScrollTime = currentTime;
        }
    }


    public class OrderInfo
    {
        private string orderID;
        private string customerID;
        private string customer;
        private string shipCity;
        private string shipCountry;

        public string OrderID
        {
            get { return orderID; }
            set { this.orderID = value; }
        }

        public string CustomerID
        {
            get { return customerID; }
            set { this.customerID = value; }
        }

        public string ShipCountry
        {
            get { return shipCountry; }
            set { this.shipCountry = value; }
        }

        public string Customer
        {
            get { return this.customer; }
            set { this.customer = value; }
        }

        public string ShipCity
        {
            get { return shipCity; }
            set { this.shipCity = value; }
        }

        public OrderInfo(string orderId, string customerId, string country, string customer, string shipCity)
        {
            this.OrderID = orderId;
            this.CustomerID = customerId;
            this.Customer = customer;
            this.ShipCountry = country;
            this.ShipCity = shipCity;
        }
    }

    public class OrderInfoRepository
    {
        string[] names = {
        "James Smith", "Michael Johnson", "Robert Williams", "David Brown", "Richard Jones",
        "Joseph Miller", "Thomas Davis", "Charles Garcia", "Daniel Rodriguez", "Paul Wilson",
        "Mark Martinez", "Donald Anderson", "George Taylor", "Kenneth Thomas", "Steven Hernandez",
        "Edward Moore", "Brian Jackson", "Ronald White", "Anthony Harris", "Kevin Martin",
        "Jason Thompson", "Matthew Robinson", "Gary Clark", "Timothy Lewis", "Jose Walker",
        "Larry Perez", "Jeffrey Hall", "Frank Young", "Scott Allen", "Eric Sanchez",
        "Stephen Wright", "Andrew King", "Raymond Scott", "Gregory Hill", "Joshua Green",
        "Jerry Adams", "Dennis Baker", "Walter Gonzalez", "Patrick Nelson", "Peter Carter",
        "Harold Mitchell", "Douglas Perez", "Carl Roberts", "Henry Turner", "Roger Phillips",
        "Keith Evans", "Gerald Diaz", "Jeremy Cruz", "Terry Edwards", "Lawrence Reyes",
        "Emma Davis", "Olivia Miller", "Ava Wilson", "Sophia Moore", "Isabella Taylor",
        "Charlotte Anderson", "Mia Thomas", "Amelia Jackson", "Harper White", "Evelyn Harris",
        "Abigail Martin", "Emily Thompson", "Elizabeth Robinson", "Mila Clark", "Ella Lewis",
        "Camila Walker", "Avery Perez", "Sofia Hall", "Scarlett Young", "Victoria Allen",
        "Madison Sanchez", "Luna Wright", "Grace King", "Chloe Scott", "Penelope Hill",
        "Layla Green", "Riley Adams", "Zoey Baker", "Nora Gonzalez", "Lily Nelson"
    };

        string[] countries = {
        "USA", "Canada", "UK", "Germany", "France", "Spain", "Italy", "Japan", "Australia", "Brazil",
        "Mexico", "China", "India", "Russia", "South Africa", "Sweden", "Norway", "Denmark", "Finland", "Netherlands"
    };

        string[] cities = {
        "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego",
        "Dallas", "San Jose", "Austin", "Jacksonville", "Fort Worth", "Columbus", "San Francisco", "Charlotte",
        "Indianapolis", "Seattle", "Denver", "Washington", "Boston", "El Paso", "Nashville", "Portland", "Oklahoma City",
        "Toronto", "Montreal", "Vancouver", "Calgary", "Ottawa", "London", "Manchester", "Liverpool", "Birmingham", "Glasgow",
        "Berlin", "Munich", "Hamburg", "Cologne", "Frankfurt", "Paris", "Marseille", "Lyon", "Toulouse", "Nice",
        "Madrid", "Barcelona", "Valencia", "Seville", "Zaragoza", "Rome", "Milan", "Naples", "Turin", "Palermo"
    };

        string[] customerIDs = {
        "ALFKI", "ANATR", "ANTON", "AROUT", "BERGS", "BLAUS", "BLONP", "BOLID", "BONAP", "BOTTM",
        "BSBEV", "CACTU", "CENTC", "CHOPS", "COMMI", "CONSH", "DRACD", "DUMON", "EASTC", "ERNSH",
        "FAMIA", "FISSA", "FOLIG", "FOLKO", "FRANK", "FRANR", "FRANS", "FURIB", "GALED", "GODOS",
        "GOURL", "GREAL", "GROSR", "HANAR", "HILAA", "HUNGC", "HUNGO", "ISLAT", "KOENE", "LACOR",
        "LAMAI", "LAUGB", "LAZYK", "LEHMS", "LETSS", "LILAS", "LINOD", "LONEP", "MAGAA", "MAISD",
        "MEREP", "MORGK", "NORTS", "OCEAN", "OLDWO", "OTTIK", "PARIS", "PERIC", "PICCO", "PRINI",
        "QUEDE", "QUEEN", "QUICK", "RANCH", "RATTC", "REGGC", "RICAR", "RICSU", "ROMEY", "SANTG",
        "SAVEA", "SEVES", "SIMOB", "SPECD", "SPLIR", "SUPRD", "THEBI", "THECR", "TOMSP", "TORTU",
        "TRADH", "TRAIH", "VAFFE", "VICTE", "VINET", "WANDK", "WARTH", "WELLI", "WHITC", "WILMK",
        "WOLZA", "GREAL", "HOMEV", "LIVCO", "LUXCO", "MTECH", "NEWID", "OMNIW", "PIXEL", "QUANT"
    };

        Random random = new Random();

        private ObservableCollection<OrderInfo> orderInfo;
        public ObservableCollection<OrderInfo> OrderInfoCollection
        {
            get { return orderInfo; }
            set { this.orderInfo = value; }
        }

        public OrderInfoRepository()
        {
            orderInfo = new ObservableCollection<OrderInfo>();
            this.GenerateOrders();
        }

        public void GenerateOrders()
        {
            for (int i = 1; i <= 100; i++)
            {
                int nameIndex = random.Next(names.Length);
                int countryIndex = random.Next(countries.Length);
                int cityIndex = random.Next(cities.Length);
                int customerIDIndex = random.Next(customerIDs.Length);

                string orderID = (1000 + i).ToString();
                string name = names[nameIndex];
                string country = countries[countryIndex];
                string customerID = customerIDs[customerIDIndex];
                string city = cities[cityIndex];

                orderInfo.Add(new OrderInfo(orderID, name, country, customerID, city));
            }
        }
    }
}
