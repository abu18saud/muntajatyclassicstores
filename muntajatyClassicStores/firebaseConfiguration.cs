using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores
{
    public class firebaseConfiguration
    {
        public readonly static string AuthSecret;
        public readonly static string BasePath;
        public readonly static string apiKey;
        public readonly static string Bucket;
        public readonly static string AuthEmail;
        public readonly static string AuthPassword;




        static firebaseConfiguration()
        {
            AuthSecret = "BMfVXkphgCXyKxUeNJuwspHRIbXCkgrU4FP5ZEcn";
            BasePath = "https://muntajatyclassic.firebaseio.com/";
            apiKey = "AIzaSyBhvltHvwwIzV5RJL1z4zBJB9E-51VEaH0";
            Bucket = "muntajatyclassic.appspot.com";
            AuthEmail = "progpro2017@gmail.com";
            AuthPassword = "Qpm1N9y8";
        }
    }
}