﻿using HRManagment.MVC.Models;
using HRManagment.MVC.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRManagment.MVC.Contarcts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVM>> GetLeaveTypes();
        Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
        Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType);
        Task<Response<int>> UpdateLeaveType(int id,LeaveTypeVM  leaveType);
        Task<Response<int>> DeleteLeaveType(int id);
    }
}
