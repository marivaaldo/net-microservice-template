namespace NetMicroserviceTemplate.API.Endpoints
{
    internal interface IEndpointsMapping
    {
        string Prefix { get; }
        string DisplayName { get; }
        void Map(RouteGroupBuilder group);
    }
}
