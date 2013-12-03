﻿using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Dev2.Services.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev2.Infrastructure.Tests.Services.Security
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class WindowsGroupPermissionComparerTests
    {
        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Constructor")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WindowsGroupPermissionComparer_Constructor_SortMemberPathIsNull_ThrowsArgumentNullException()
        {
            //------------Setup for test-------------------------

            //------------Execute Test---------------------------
            var comparer = new WindowsGroupPermissionComparer(ListSortDirection.Ascending, null);

            //------------Assert Results-------------------------
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_XorYNull_Returns1()
        {
            //------------Setup for test-------------------------
            var comparer = new WindowsGroupPermissionComparer(ListSortDirection.Ascending, "WindowsGroup");

            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            var result = comparer.Compare(null, null);
            Assert.AreEqual(1, result);

            result = comparer.Compare(new WindowsGroupPermission(), null);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_XIsNew_ReturnsMaxValue()
        {
            //------------Setup for test-------------------------
            var comparer = new WindowsGroupPermissionComparer(ListSortDirection.Ascending, "WindowsGroup");

            //------------Execute Test---------------------------
            var result = comparer.Compare(new WindowsGroupPermission { IsNew = true }, new WindowsGroupPermission());

            //------------Assert Results-------------------------
            Assert.AreEqual(int.MaxValue, result);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_YIsNew_ReturnsMinValue()
        {
            //------------Setup for test-------------------------
            var comparer = new WindowsGroupPermissionComparer(ListSortDirection.Ascending, "WindowsGroup");

            //------------Execute Test---------------------------
            var result = comparer.Compare(new WindowsGroupPermission(), new WindowsGroupPermission { IsNew = true });

            //------------Assert Results-------------------------
            Assert.AreEqual(int.MinValue, result);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_XIsBuiltInAdministrators_ReturnsMinValue()
        {
            //------------Setup for test-------------------------
            var comparer = new WindowsGroupPermissionComparer(ListSortDirection.Ascending, "WindowsGroup");

            //------------Execute Test---------------------------
            var result = comparer.Compare(new WindowsGroupPermission { IsServer = true, WindowsGroup = WindowsGroupPermission.BuiltInAdministratorsText }, new WindowsGroupPermission());

            //------------Assert Results-------------------------
            Assert.AreEqual(int.MinValue, result);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_YIsBuiltInAdministrators_ReturnsMaxValue()
        {
            //------------Setup for test-------------------------
            var comparer = new WindowsGroupPermissionComparer(ListSortDirection.Ascending, "WindowsGroup");

            //------------Execute Test---------------------------
            var result = comparer.Compare(new WindowsGroupPermission(), new WindowsGroupPermission { IsServer = true, WindowsGroup = WindowsGroupPermission.BuiltInAdministratorsText });

            //------------Assert Results-------------------------
            Assert.AreEqual(int.MaxValue, result);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortWindowsGroupAscending_ReturnsCorrectValue()
        {
            Verify_Compare(1, ListSortDirection.Ascending, "WindowsGroup",
                new WindowsGroupPermission { WindowsGroup = "c" }, new WindowsGroupPermission { WindowsGroup = "a" });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortWindowsGroupDescending_ReturnsCorrectValue()
        {
            Verify_Compare(-1, ListSortDirection.Descending, "WindowsGroup",
                new WindowsGroupPermission { WindowsGroup = "c" }, new WindowsGroupPermission { WindowsGroup = "a" });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortResourceNameAscending_ReturnsCorrectValue()
        {
            Verify_Compare(1, ListSortDirection.Ascending, "ResourceName",
                new WindowsGroupPermission { ResourceName = "c" }, new WindowsGroupPermission { ResourceName = "a" });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortResourceNameDescending_ReturnsCorrectValue()
        {
            Verify_Compare(-1, ListSortDirection.Descending, "ResourceName",
                new WindowsGroupPermission { ResourceName = "c" }, new WindowsGroupPermission { ResourceName = "a" });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortViewAscending_ReturnsCorrectValue()
        {
            Verify_Compare(-1, ListSortDirection.Ascending, "View",
                new WindowsGroupPermission { View = false }, new WindowsGroupPermission { View = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortViewDescending_ReturnsCorrectValue()
        {
            Verify_Compare(1, ListSortDirection.Descending, "View",
                new WindowsGroupPermission { View = false }, new WindowsGroupPermission { View = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortExecuteAscending_ReturnsCorrectValue()
        {
            Verify_Compare(-1, ListSortDirection.Ascending, "Execute",
                new WindowsGroupPermission { Execute = false }, new WindowsGroupPermission { Execute = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortExecuteDescending_ReturnsCorrectValue()
        {
            Verify_Compare(1, ListSortDirection.Descending, "Execute",
                new WindowsGroupPermission { Execute = false }, new WindowsGroupPermission { Execute = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortContributeAscending_ReturnsCorrectValue()
        {
            Verify_Compare(-1, ListSortDirection.Ascending, "Contribute",
                new WindowsGroupPermission { Contribute = false }, new WindowsGroupPermission { Contribute = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortContributeDescending_ReturnsCorrectValue()
        {
            Verify_Compare(1, ListSortDirection.Descending, "Contribute",
                new WindowsGroupPermission { Contribute = false }, new WindowsGroupPermission { Contribute = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortDeployAscending_ReturnsCorrectValue()
        {
            Verify_Compare(-1, ListSortDirection.Ascending, "DeployTo",
                new WindowsGroupPermission { DeployTo = false }, new WindowsGroupPermission { DeployTo = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortDeployToDescending_ReturnsCorrectValue()
        {
            Verify_Compare(1, ListSortDirection.Descending, "DeployTo",
                new WindowsGroupPermission { DeployTo = false }, new WindowsGroupPermission { DeployTo = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortDeployFromAscending_ReturnsCorrectValue()
        {
            Verify_Compare(-1, ListSortDirection.Ascending, "DeployFrom",
                new WindowsGroupPermission { DeployFrom = false }, new WindowsGroupPermission { DeployFrom = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortDeployFromDescending_ReturnsCorrectValue()
        {
            Verify_Compare(1, ListSortDirection.Descending, "DeployFrom",
                new WindowsGroupPermission { DeployFrom = false }, new WindowsGroupPermission { DeployFrom = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortAdministratorAscending_ReturnsCorrectValue()
        {
            Verify_Compare(-1, ListSortDirection.Ascending, "Administrator",
                new WindowsGroupPermission { Administrator = false }, new WindowsGroupPermission { Administrator = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortAdministratorDescending_ReturnsCorrectValue()
        {
            Verify_Compare(1, ListSortDirection.Descending, "Administrator",
                new WindowsGroupPermission { Administrator = false }, new WindowsGroupPermission { Administrator = true });
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortOtherAscending_ReturnsCorrectValue()
        {
            Verify_Compare(0, ListSortDirection.Ascending, "Other",
                new WindowsGroupPermission(), new WindowsGroupPermission());
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("WindowsGroupPermissionComparer_Compare")]
        public void WindowsGroupPermissionComparer_Compare_SortOtherDescending_ReturnsCorrectValue()
        {
            Verify_Compare(0, ListSortDirection.Descending, "Other",
                new WindowsGroupPermission(), new WindowsGroupPermission());
        }

        void Verify_Compare(int expected, ListSortDirection direction, string sortMemberPath, WindowsGroupPermission px, WindowsGroupPermission py)
        {
            //------------Setup for test-------------------------
            var comparer = new WindowsGroupPermissionComparer(direction, sortMemberPath);

            //------------Execute Test---------------------------
            var result = comparer.Compare(px, py);

            //------------Assert Results-------------------------
            Assert.AreEqual(expected, result);
        }
    }
}