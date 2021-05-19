using System;

namespace BlogApi.Exceptions
{
    public class BlogPostsDomainException : Exception
    {
        public BlogPostsDomainException()
        { }

        public BlogPostsDomainException(string message)
            : base(message)
        { }

        public BlogPostsDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
