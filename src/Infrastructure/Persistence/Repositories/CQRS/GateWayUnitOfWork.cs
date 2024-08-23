using AutoMapper;
using DrMadWill.Layers.Repository.Concretes.CQRS;
using Persistence.Context;

namespace Persistence.Repositories.CQRS;

public class GateWayUnitOfWork : UnitOfWork
{
    public GateWayUnitOfWork(GateWayContext orgContext, IMapper mapper) : base(orgContext, typeof(ServiceRegistration),mapper)
    {
    }
}