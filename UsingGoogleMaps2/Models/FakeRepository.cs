using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsingGoogleMaps2.Models
{
   public class FakeRepository:IRepository
    {
       public static List<Company> companyFakeTable = new List<Company>()
      {
            new Company{   
                            Id=1,Name="O'Reilly's",Contact="oreillys@gmail.com",Businesses=new List<Business>()
                                {
                                new Business
                                    {   
                                        Id=1,Category=Categories.Pub.ToString(),Name="O'Reilly's",FK_Company=1,
                                        EEVAs=new List<EEVA>()
                                        {
                                        new EEVA{Id=1, FK_Business=1, Attribute="Longitude", Value="-6.254101"},
                                        new EEVA{Id=2, FK_Business=1, Attribute="Latitude", Value="53.347260"},
                                        new EEVA{Id=3, FK_Business=1, Attribute="Opening Hours", Value="9-7, M-F"}
                                        }
                                    } 
                                }
                             
                            
                        },
            
    
             new Company{   
                            Id=2,Name="Dicey's Garden",Contact="DiceysGarden@gmail.com",Businesses=new List<Business>()
                                {
                                new Business
                                    {   
                                        Id=2,Category=Categories.Pub.ToString(),Name="Dicey's Garden",FK_Company=2,
                                        EEVAs=new List<EEVA>()
                                        {
                                        new EEVA{Id=4, FK_Business=2, Attribute="Longitude", Value="-6.263532"},
                                        new EEVA{Id=5, FK_Business=2, Attribute="Latitude", Value="53.335983"},
                                        new EEVA{Id=6, FK_Business=2, Attribute="Opening Hours", Value="9-1,2-5, M-F"}
                                        }
                                    } 
                                }
                             
                            
                        },
           
            
               new Company{   
                            Id=3,Name="Howl at the moon",Contact="HowlAtTheMoon@gmail.com",Businesses=new List<Business>()
                                {
                                new Business
                                    {   
                                        Id=3,Category=Categories.Pub.ToString(),Name="Howl at the moon",FK_Company=1,
                                        EEVAs=new List<EEVA>()
                                        {
                                        new EEVA{Id=7, FK_Business=3, Attribute="Longitude", Value="-6.245727"},
                                        new EEVA{Id=8, FK_Business=3, Attribute="Latitude", Value="53.339426"},
                                        new EEVA{Id=9, FK_Business=3, Attribute="Opening Hours", Value="9-5, M-F"}
                                        }
                                    } 
                                }
                             
                            
                        },
            new Company{   
                            Id=4,Name="Darkey kelly's",Contact="Darkeykellys@gmail.com",Businesses=new List<Business>()
                                {
                                new Business
                                    {   
                                        Id=4,Category=Categories.Pub.ToString(),Name="Darkey kelly's",FK_Company=1,
                                        EEVAs=new List<EEVA>()
                                        {
                                        new EEVA{Id=10, FK_Business=4, Attribute="Longitude", Value="-6.270058"},
                                        new EEVA{Id=11, FK_Business=4, Attribute="Latitude", Value="53.344067"},
                                        new EEVA{Id=12, FK_Business=4, Attribute="Opening Hours", Value="10-6, M-F"}
                                        }
                                    } 
                                }
                             
                            
                        }
                  
       
      };
       public static List<Pub> pubFakeTable = new List<Pub>()
       {
           new Pub{ Id=1, Name="Howl at the moon", Address="7/8 Lower Mount Street, Dublin 2", PostCode=PostCodes.D2, Info="9-5, M-F", Latitude=53.339426, Longitude=-6.245727 },
           new Pub{ Id=2, Name="Dicey's Garden",Address="21-25 Harcourt St, Dublin 2", PostCode=PostCodes.D2, Info="9-1,2-5, M-F", Latitude=53.335983, Longitude=-6.263532 },
           new Pub{ Id=3, Name="O'Reilly's", Address="George's Quay, Dublin 2", PostCode=PostCodes.D2,Info="9-7, M-F", Latitude=53.347260, Longitude=-6.254101 },
           new Pub{ Id=4, Name="Darkey kelly's",Address="19 Fishamble St, Dublin 8", PostCode=PostCodes.D8, Info="10-6, M-F", Latitude=53.344067, Longitude=-6.270058 }
           
       };


       public IEnumerable<Pub> ShowPubsOnMap(PostCodes postcode)
       {
           return pubFakeTable.Where(Entries => Entries.PostCode == postcode);
       }

       public IEnumerable<Pub> ShowAllPubsOnMap()
       {
           return pubFakeTable.Select(Entries => Entries);
       }
    }
}



  