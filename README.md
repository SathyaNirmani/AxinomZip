# AxinomZip
Axinom Zip
===============

*******************************************************************************************************
GENERAL USAGE NOTES 
*****************************************************************************************************

*******************************************************************************************************
Technical Information 
*******************************************************************************************************

Technologies used
 C# Asp.Net Core, Entity Framework Core, HTML 5,CSS, SQL, Jquery , Java Script

******************************************************************************************************
IMPORTANT CREDENTIALS
******************************************************************************************************


Username: Axinom (login)
Password: 1234


********************************************************************************************************	
SETUP AND RUN
********************************************************************************************************

Instructions to run the project
==========================================================


Software(install) - Visual Studio 2017 , Sql Server 2014


1. Setup connection string in AppSetting of AxinomZip.Api to your local database 

• AxinomZip.Api
  "ConnectionStrings": {
    "AxinomConnectionString": "Server=ServerName;Database=AxinomZipDb;MultipleActiveResultSets=true"
  },

 3.If connecting to local database set a local database connection string.
  'Update Database' using migrations. 
   Seed data will automatically added during migrations. 

4. AxinomZip.Web and AxinomZip.Api are already setup as startup projects

5. Run the project

********************************************************************************************************