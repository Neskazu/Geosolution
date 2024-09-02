using CityInfo.Models;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Text;

namespace CityInfo.Services
{
    public class GeoServerService
    {
        private readonly HttpClient httpClient;
        private readonly string geoServerUrl;
        private readonly string workSpace;
        private readonly string layerName;

        public GeoServerService(string geoServerUrl, string workSpace, string layerName)
        {
            httpClient = new HttpClient();
            this.geoServerUrl = geoServerUrl;
            this.workSpace = workSpace;
            this.layerName = layerName;
        }
        public async Task AddCityObjectAsync(CityObjectModel cityObjectModel)
        {
            var featureXml = $@"
    <wfs:Transaction service='WFS' version='1.0.0'
                     xmlns:wfs='http://www.opengis.net/wfs'
                     xmlns:gml='http://www.opengis.net/gml'
                     xmlns:ogc='http://www.opengis.net/ogc'>
        <wfs:Insert>
            <feature:{layerName} xmlns:feature='http://your-geoserver-url/geoserver/{workSpace}'>
                <feature:geometry>
                    <gml:Point srsName='EPSG:4326'>
                        <gml:coordinates>{cityObjectModel.Longitude},{cityObjectModel.Latitude}</gml:coordinates>
                    </gml:Point>
                </feature:geometry>
                <feature:name>{cityObjectModel.Name}</feature:name>
                <feature:description>{cityObjectModel.Description}</feature:description>
            </feature:{layerName}>
        </wfs:Insert>
    </wfs:Transaction>";

            var content = new StringContent(featureXml, Encoding.UTF8, "application/xml");

            var requestUri = $"{geoServerUrl}/geoserver/wfs";
            var response = await httpClient.PostAsync(requestUri, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Geoserver Error: {responseContent}");
            }
            else
            {
                Console.WriteLine($"Geoserver Response: {responseContent}");
            }
        }
    }
}
