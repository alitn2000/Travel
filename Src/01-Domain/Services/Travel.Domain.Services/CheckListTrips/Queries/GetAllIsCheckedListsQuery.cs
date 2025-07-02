using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.DTOs.CheckListTripDtos;

namespace Travel.Domain.Service.CheckListTrips.Queries;

public class GetAllIsCheckedListsQuery : IRequest<List<CheckListTripListDto>>
{
}
