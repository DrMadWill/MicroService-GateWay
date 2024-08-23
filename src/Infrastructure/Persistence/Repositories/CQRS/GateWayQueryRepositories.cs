using AutoMapper;
using DrMadWill.Layers.Repository.Concretes.CQRS;
using Persistence.Context;

namespace Persistence.Repositories.CQRS;

public class GateWayQueryRepositories : QueryRepositories 
{
    public GateWayQueryRepositories(GateWayContext orgContext, IMapper mapper) : base(orgContext, mapper, typeof(ServiceRegistration))
    {
    }
}