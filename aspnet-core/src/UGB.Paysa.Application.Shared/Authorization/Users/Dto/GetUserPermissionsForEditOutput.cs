﻿using System.Collections.Generic;
using UGB.Paysa.Authorization.Permissions.Dto;

namespace UGB.Paysa.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}