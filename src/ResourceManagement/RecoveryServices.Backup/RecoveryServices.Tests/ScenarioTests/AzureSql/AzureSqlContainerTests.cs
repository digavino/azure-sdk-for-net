﻿//
// Copyright (c) Microsoft.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using Hyak.Common;
using Microsoft.Azure;
using Microsoft.Azure.Management.RecoveryServices.Backup;
using Microsoft.Azure.Management.RecoveryServices.Backup.Models;
using Microsoft.Azure.Test;
using RecoveryServices.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RecoveryServices.Tests
{
    public class AzureSqlContainerTests: RecoveryServicesTestsBase
    {
        [Fact]
        public void ListAzureSqlContainersTest()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();

                string resourceNamespace = ConfigurationManager.AppSettings["ResourceNamespace"];

                var client = GetServiceClient<RecoveryServicesBackupManagementClient>(resourceNamespace);
                ProtectionContainerListQueryParams queryParams = new ProtectionContainerListQueryParams();
                queryParams.BackupManagementType = BackupManagementType.AzureSql.ToString();

                ContainerTestHelper containerTestHelper = new ContainerTestHelper(client);
                ProtectionContainerListResponse response = containerTestHelper.ListContainers(queryParams);

                string containerUniqueName = ConfigurationManager.AppSettings["ContainerTypeAzureSql"] + ";" + ConfigurationManager.AppSettings["AzureSqlContainerName"];
                var container = response.ItemList.ProtectionContainers[0];
                Assert.NotNull(container);
                Assert.Equal(containerUniqueName, container.Name);
            }
        }

        [Fact]
        public void UnregisterContainersTest()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();

                string resourceNamespace = ConfigurationManager.AppSettings["ResourceNamespace"];

                var client = GetServiceClient<RecoveryServicesBackupManagementClient>(resourceNamespace);
                ContainerTestHelper containerTestHelper = new ContainerTestHelper(client);
                string sqlContainerName =  ConfigurationManager.AppSettings["ContainerTypeAzureSql"] + ";" + ConfigurationManager.AppSettings["AzureSqlContainerName"];
                AzureOperationResponse response = containerTestHelper.UnregisterMABContainer(sqlContainerName);
                Assert.Equal(response.StatusCode, HttpStatusCode.NoContent);
            }
        }
    }
}
