namespace MvcMarket.BackEnd
{
    using System.ComponentModel;

    public partial class ShippingDetails : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                if ((columnName == "Name") && string.IsNullOrEmpty(Name))
                    return "Please enter a name";
                if ((columnName == "Country") && string.IsNullOrEmpty(Country))
                    return "Please enter a country name";
                if ((columnName == "State") && string.IsNullOrEmpty(State))
                    return "Please enter a state name";
                if ((columnName == "City") && string.IsNullOrEmpty(City))
                    return "Please enter a city name";
                if ((columnName == "Address") && string.IsNullOrEmpty(Address))
                    return "Please enter a address";
                if ((columnName == "Zip") && string.IsNullOrEmpty(Zip))
                    return "Please enter a zip";
                return null;
            }
        }

        public string Error
        {
            get { return null; }
        }
    }
}