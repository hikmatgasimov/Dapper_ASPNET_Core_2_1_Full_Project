﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using AutoMapper;
using HRManagment.Application.Profiles;
using HRManagment.Application.Contracts.Persistence;
using HRManagment.UnitTests.Mocks;
using Xunit;
using HRManagment.Application.Features.LeaveTypes.Handlers.Commands;
using System.Threading;
using HRManagment.Application.Features.LeaveTypes.Request.Queries;
using HRManagment.Application.DTOs.LeaveType;
using HRManagment.Application.Exception;
using HRManagment.Application.Responses;

namespace HRManagment.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeListCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly CreateLeaveTypeDto _leaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _handler;
        public CreateLeaveTypeListCommandHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler=new  CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);  //invoke
            _leaveTypeDto = new CreateLeaveTypeDto
            {
             
                DefaultDays = 15,
                Name = "Test DTO"
            };

        }
        [Fact]
        public async Task Valid_LeaveType_Added()
        {
            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockRepo.Object.GetAll();
            result.ShouldBeOfType<BaseCommandResponse>();

            leaveTypes.Count.ShouldBe(4);
        }
        [Fact]
        public async Task InValid_LeaveType_Added()
        {
            _leaveTypeDto.DefaultDays = -1;
            ValidationException ex = await Should.ThrowAsync<ValidationException>
                (async () =>
                await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None)
                );
            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockRepo.Object.GetAll();
         

            leaveTypes.Count.ShouldBe(3);

            result.ShouldBeOfType<BaseCommandResponse>();
        }
    }
}
