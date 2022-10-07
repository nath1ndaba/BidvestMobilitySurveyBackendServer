namespace BidvestMobilitySurveyBackendServer.Services;

public interface IUpdateBuilderProvider
{
    IUpdateBuilder<TType> For<TType>();
}