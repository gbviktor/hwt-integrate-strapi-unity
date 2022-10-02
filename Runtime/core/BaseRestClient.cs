namespace com.gbviktor.hwtintegratestrapiunity.core
{
    public class BaseRestClient<T>
    {
        protected const string GET = "GET";
        protected const string POST = "POST";
        protected const string PUT = "PUT";
        protected const string DELETE = "DELETE";

        protected T transport;
        protected string endpoint;
    }
}
