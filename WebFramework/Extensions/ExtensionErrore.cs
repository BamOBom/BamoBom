using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebFramework.Extensions
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            var extension = Path.GetExtension(file.FileName);
            if (file != null)
            {
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"فرمت عکس مورد قبول نسیت!";
        }
    }
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            //var extension = Path.GetExtension(file.FileName);
            //var allowedExtensions = new[] { ".jpg", ".png" };`enter code here`
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"بشترین سایز مجاز برای عکس { _maxFileSize} bytes.";
        }
    }
    public class CustomCompressionProvider : ICompressionProvider
    {
        public string EncodingName => "mycustomcompression";
        public bool SupportsFlush => true;

        public Stream CreateStream(Stream outputStream)
        {
            // Create a custom compression stream wrapper here
            return outputStream;
        }
    }
    public class RedirectImageRequests : IRule
    {
        private readonly string _extension;
        private readonly PathString _newPath;

        public RedirectImageRequests(string extension, string newPath)
        {
            if (string.IsNullOrEmpty(extension))
            {
                throw new ArgumentException(nameof(extension));
            }

            if (!Regex.IsMatch(extension, @"^\.(png|jpg|gif)$"))
            {
                throw new ArgumentException("Invalid extension", nameof(extension));
            }

            if (!Regex.IsMatch(newPath, @"(/[A-Za-z0-9]+)+?"))
            {
                throw new ArgumentException("Invalid path", nameof(newPath));
            }

            _extension = extension;
            _newPath = new PathString(newPath);
        }

        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;

            // Because we're redirecting back to the same app, stop 
            // processing if the request has already been redirected
            if (request.Path.StartsWithSegments(new PathString(_newPath)))
            {
                return;
            }

            if (request.Path.Value.EndsWith(_extension, StringComparison.OrdinalIgnoreCase))
            {
                var response = context.HttpContext.Response;
                response.StatusCode = StatusCodes.Status301MovedPermanently;
                context.Result = RuleResult.EndResponse;
                response.Headers[HeaderNames.Location] =
                    _newPath + request.Path + request.QueryString;
            }
        }
        public static void RewriteTextFileRequests(RewriteContext context)
        {
            var request = context.HttpContext.Request;

            if (request.Path.Value.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = RuleResult.SkipRemainingRules;
                request.Path = "/file.txt";
            }
        }
        public static void RedirectXmlFileRequests(RewriteContext context)
        {
            var request = context.HttpContext.Request;

            // Because the client is redirecting back to the same app, stop 
            // processing if the request has already been redirected.
            if (request.Path.StartsWithSegments(new PathString("/xmlfiles")))
            {
                return;
            }

            if (request.Path.Value.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                var response = context.HttpContext.Response;
                response.StatusCode = StatusCodes.Status301MovedPermanently;
                context.Result = RuleResult.EndResponse;
                response.Headers[HeaderNames.Location] =
                    "/xmlfiles" + request.Path + request.QueryString;
            }
        }
    }
}
