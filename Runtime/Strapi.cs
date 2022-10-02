using com.gbviktor.hwtintegratestrapiunity.core;

namespace com.gbviktor.hwtintegratestrapiunity
{
    public class Strapi
    {
        StrapiServerConfig config;
        RestAPITransportSync transport;

        public Strapi(StrapiServerConfig conf)
        {
            ApplyConfigToTransport(conf);
        }

        private void ApplyConfigToTransport(StrapiServerConfig conf)
        {
            this.config = conf;
            transport = new RestAPITransportSync(config);
        }

        public Strapi(string strapiUrl, string apiToken)
        {
            this.config = new StrapiServerConfig();
            this.config.Token = apiToken;
            this.config.URL = strapiUrl;
            ApplyConfigToTransport(config);
        }

        public void RegisterNewUser(string email, string name, string password)
        {
            //api/auth/local
            //TODO
        }
        public void Login(string email, string password)
        {
            //TODO 
        }

        /// <summary>
        /// Method to create Repositories
        /// </summary>
        /// <typeparam name="T">Any type inherited of IStrapiEntityType</typeparam>
        /// <param name="endpoint">Path to endpoint after base strapiUrl setted in config</param>
        /// <returns>Repository to operate with data by calling REST Api endpoints</returns>
        public Repository<T> CreateRepository<T>(string endpoint) where T : IStrapiEntityType
        {
            return new Repository<T>(endpoint, transport);
        }
    }
}
