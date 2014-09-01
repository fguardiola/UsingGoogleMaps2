using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsingGoogleMaps2.Models
{
    
     public class SearchPreferences
    {
         public static List<string> Areas
         {
             get
             {
                 return new List<string>
                        {
                            "Adamstown ","Artane ","Ashtown ","Aylesbury ","Balbriggan ",
                             "Baldonnell ","Baldoyle ","Balgriffin ","Ballinteer ","Ballsbridge ","Ballybough","Ballyboughal ","Ballybrack","Ballycullen","Ballyfermot","Ballymount","Ballymun",
                             "Balrothery","Bayside","Beaumont","Belfield","Blackrock","Blanchardstown","Bluebell","Booterstown","Cabinteely","Cabra","Carpenterstown","Carrickmines","Castleknock","Chapelizod",
                             "Cherry Orchard","Christchurch","Churchtown","Citywest","Clarehall","Clondalkin","Clonee","Clongriffin","Clonshaugh","Clonsilla","Clonskeagh","Clontarf","Coolmine","Coolock",
                             "Crumlin","Dalkey","Dartry","Deans Grange","Donabate","Donaghmede","Donnybrook","Donnycarney","Drimnagh","Drumcondra", "Dun Laoghaire","Dundrum","East Wall","Fairview","Finglas",
                             "Firhouse","Foxrock","Galloping Green","Glasnevin","Glenageary","Goatstown","Grand Canal Dock","Hanover Quay","Harold's Cross","Hartstown","Hollystown","Howth","IFSC","Inchicore",
                             "Irishtown","Islandbridge","Killester","Killiney","Kilmacud","Kilmainham","Kiltipper","Kimmage","Kingswood","Knocklyon","Leopardstown","Loughlinstown","Loughshinny","Lucan","Lusk",
                              "Malahide","Marino","Milltown","Monkstown","Mount Merrion","Mulhuddart","Naul","Navan Road (D7)","Newcastle","North Circular Road","North Strand","North Wall","Ongar","Palmerstown",
                            "Park West","Phibsborough","Portmarnock","Portobello","Raheny","Ranelagh","Rathcoole","Rathfarnham","Rathgar","Rathmichael","Rathmines","Rialto","Ringsend","Rush","Saggart",
                            "Sandycove","Sandyford","Sandymount","Santry","Shankill","Skerries","Smithfield","South Circular Road","Stepaside","Stillorgan","Stoneybatter","Sutton","Swords","Tallaght",
                            "Temple Bar","Templeogue","Terenure","The Coombe","Tyrrelstown","Walkinstown","Whitehall",
                        };
             }
         }

         public static List<string> MaxPrices
         {
             get
             {
                 return new List<string>
                        {
                            "No max", "€5", "€10", "€15", "€20", "€25", "€30", "€35", "€40", "€45", "€50", "€55", "€60", "€65", "€70", "€75", "€80", "€85", "€90", "€95", "€100" 
                        };
             }
         }
         public static List<string> DayOfWeek
         {
             get
             {
                 return new List<string>
                        {
                            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Every Monday", "Every Tuesday", "Every Wednesday", "Every Thursday", "Every Friday", "Every Saturday", "Every Sunday","Every day" 
                        };
             }
         }

         public static List<string> PropertyTypes
         {
             get
             {
                 return new List<string>
                        {
                            "Pub", "Restaurant", "Pub & Restaurant"
                        };
             }
         }
        public static decimal  PriceMaxToDecimal(string pricemax)
        {
            switch (pricemax)
	            {
		            case "No max":
                    return Decimal.MaxValue;
                    //break;
                    case "€5":
                    return 5;
                    case "€10":
                    return 10;
                    case "€15":
                    return 15;
                    case "€20":
                    return 20;
                    case "€25":
                    return 25;
                    case "€30":
                    return 30;
                    case "€35":
                    return 35;
                    case "€40":
                    return 40;
                    case "€45":
                    return 45;
                    case "€50":
                    return 50;
                    case "€55":
                    return 55;
                    case "€60":
                    return 60;
                    case "€65":
                    return 65;
                    case "€70":
                    return 70;
                    case "€75":
                    return 75;
                    case "€80":
                    return 80;
                    case "€85":
                    return 85;
                    case "€90":
                    return 90;
                    case "€95":
                    return 95;
                    case "€100":
                    return 100;
                    default:
                    return 0;
                   
	            }
        }
         public string Area { get; set; }
        
        public string PriceMax { get; set; }
        public string PropertyType { get; set; }

        
    
    }
}



/*"- Dublin City Centre -","- North Dublin City -","- South Dublin City -","- North Co. Dublin -","- South Co. Dublin -","- West Co. Dublin -","- Dublin Commuter Towns -",
                            "Dublin 1","Dublin 2","Dublin 3","Dublin 4","Dublin 5","Dublin 6","Dublin 6w","Dublin 7","Dublin 8","Dublin 9","Dublin 10","Dublin 11","Dublin 12","Dublin 13",
                             "Dublin 14","Dublin 15","Dublin 16","Dublin 17","Dublin 18","Dublin 20","Dublin 22" ,"Dublin 24","Adamstown ","Artane ","Ashtown ","Aylesbury ","Balbriggan ",
                             "Baldonnell ","Baldoyle ","Balgriffin ","Ballinteer ","Ballsbridge ","Ballybough","Ballyboughal ","Ballybrack","Ballycullen","Ballyfermot","Ballymount","Ballymun",
                             "Balrothery","Bayside","Beaumont","Belfield","Blackrock","Blanchardstown","Bluebell","Booterstown","Cabinteely","Cabra","Carpenterstown","Carrickmines","Castleknock","Chapelizod",
                             "Cherry Orchard","Christchurch","Churchtown","Citywest","Clarehall","Clondalkin","Clonee","Clongriffin","Clonshaugh","Clonsilla","Clonskeagh","Clontarf","Coolmine","Coolock",
                             "Crumlin","Dalkey","Dartry","Deans Grange","Donabate","Donaghmede","Donnybrook","Donnycarney","Drimnagh","Drumcondra", "Dun Laoghaire","Dundrum","East Wall","Fairview","Finglas",
                             "Firhouse","Foxrock","Galloping Green","Glasnevin","Glenageary","Goatstown","Grand Canal Dock","Hanover Quay","Harold's Cross","Hartstown","Hollystown","Howth","IFSC","Inchicore",
                             "Irishtown","Islandbridge","Killester","Killiney","Kilmacud","Kilmainham","Kiltipper","Kimmage","Kingswood","Knocklyon","Leopardstown","Loughlinstown","Loughshinny","Lucan","Lusk",
                              "Malahide","Marino","Milltown","Monkstown","Mount Merrion","Mulhuddart","Naul","Navan Road (D7)","Newcastle","North Circular Road","North Strand","North Wall","Ongar","Palmerstown",
                            "Park West","Phibsborough","Portmarnock","Portobello","Raheny","Ranelagh","Rathcoole","Rathfarnham","Rathgar","Rathmichael","Rathmines","Rialto","Ringsend","Rush","Saggart",
                            "Sandycove","Sandyford","Sandymount","Santry","Shankill","Skerries","Smithfield","South Circular Road","Stepaside","Stillorgan","Stoneybatter","Sutton","Swords","Tallaght",
                            "Temple Bar","Templeogue","Terenure","The Coombe","Tyrrelstown","Walkinstown","Whitehall",*/