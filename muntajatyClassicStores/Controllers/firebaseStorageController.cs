using Firebase.Auth;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using muntajatyClassicStores.Models;
using System.Web.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace muntajatyClassicStores.Controllers
{
    public class firebaseStorageController : Controller
    {
        private static readonly IHostingEnvironment _env;

        //public static firebaseStorageController(IHostingEnvironment env)
        //{
        //    _env = env;
        //}
        //Configure Firebase
        private static string apiKey = firebaseConfiguration.apiKey;
        private static string Bucket = firebaseConfiguration.Bucket;
        private static string AuthEmail = firebaseConfiguration.AuthEmail;
        private static string AuthPassword = firebaseConfiguration.AuthPassword;
        //--------------------------------

        //public async Task<string> uploadFile(string uniqeName, string Folder, string path)
        //{
        //    string nofification = string.Empty;
        //    string link = string.Empty;
        //    //https://www.youtube.com/watch?v=nh17WlHtODs&t=973s
        //    // Get any Stream - it can be FileStream, MemoryStream or any other type of Stream
        //    var stream = new File.Open(path, FileMode.Open);

        //    // Construct FirebaseStorage, path to where you want to upload the file and Put it there
        //    var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
        //    var a = (AuthEmail != string.Empty) ? await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword) : await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
        //    //Cancellation token
        //    var cancellation = new CancellationTokenSource();
        //    var upload = new FirebaseStorage(
        //        Bucket,
        //        new FirebaseStorageOptions
        //        {
        //            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
        //            ThrowOnCancel = true
        //        })
        //        .Child(Folder)
        //        .Child($"{uniqeName}")
        //        .PutAsync(stream, cancellation.Token)
        //        ;
        //    try
        //    {
        //        link = await upload;
        //        return link;
        //    }
        //    catch
        //    {
        //        return link;
        //    }
        //}
        public static async Task<string> abood(fileUploadViewModel file)
        {
            string link = string.Empty;
            var fileUploud = file.file;
            FileStream fs;
            if (fileUploud.Length > 0)
            {
                //Upload the file to firebase
                string foldername = "firebaseFiles";
                string path = Path.Combine(_env.WebRootPath, $"images/{foldername}");
                fs = new FileStream(Path.Combine(path, fileUploud.FileName), FileMode.Open);

                //Firebase uploading stuffs
                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                var a = (AuthEmail != string.Empty) ? await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword) : await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
                //Cancellation token
                var cancellation = new CancellationTokenSource();
                var upload = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(foldername)
                    .Child($"{fileUploud.FileName}.{Path.GetExtension(fileUploud.FileName).Substring(1)}")
                    .PutAsync(fs, cancellation.Token);
                try
                {
                    link = await upload;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"*********{ex}*******");
                    link = string.Empty;
                }
            }
            return link;
        }





        public async void deleteFile(string uniqeName, string FolderName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            //Cancellation token
            var cancellation = new CancellationTokenSource();
            try
            {
                var upload = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(FolderName)
                    .Child($"{uniqeName}")
                    .DeleteAsync();
            }
            catch
            {
            }
        }
        public async void deleteFolder(string Folder)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            //Cancellation token
            var cancellation = new CancellationTokenSource();
            try
            {
                var upload = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(Folder)
                    .DeleteAsync();
            }
            catch
            {
            }
        }
        // GET: firebaseStorage
        [HttpPost]
        public async Task<IActionResult> Index(fileUploadViewModel file)
        {
            var fileUploud = file.file;
            FileStream fs;
            if (fileUploud.Length > 0)
            {
                //Upload the file to firebase
                string foldername = "firebaseFiles";
                string path = Path.Combine(_env.WebRootPath, $"images/{foldername}");
                fs = new FileStream(Path.Combine(path, fileUploud.FileName), FileMode.Open);

                //Firebase uploading stuffs
                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                var a = (AuthEmail != string.Empty) ? await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword) : await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
                //Cancellation token
                var cancellation = new CancellationTokenSource();
                var upload = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(foldername)
                    .Child($"{fileUploud.FileName}.{Path.GetExtension(fileUploud.FileName).Substring(1)}")
                    .PutAsync(fs, cancellation.Token);
                try
                {
                    ViewBag.link = await upload;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"*********{ex}*******");
                }
            }

            return BadRequest();
        }

        private IActionResult BadRequest()
        {
            throw new NotImplementedException();
        }
    }
}