﻿using Csla;
using Csla.DataPortalClient;
using Csla.Testing.Business.ReadOnlyTest;
using System;
using Csla.Testing.Business.Security;
using UnitDriven;
using Csla.Testing.Business.EditableRootTests;
using cslalighttest.CslaDataProvider;

#if NUNIT
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestSetup = NUnit.Framework.SetUpAttribute;
#elif MSTEST
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace cslalighttest.UserStateForPortal
{
#if TESTING
  [System.Diagnostics.DebuggerNonUserCode]
#endif
  [TestClass]
  public class UserStateForPortalTests : TestBase
  {
    [TestMethod]
    public void TestCreateNewRemoteProxyWithUserState()
    {
      Csla.DataPortal.ProxyTypeName = "Csla.DataPortalClient.WcfProxy, Csla";

      UnitTestContext context = GetContext();
      object userState = "user state";
      DataPortal.BeginCreate<Customer>(new SingleCriteria<Customer, int>(2), (o, e) =>
      {
        Customer actual = e.Object;
        context.Assert.IsNull(e.Error);
        context.Assert.IsNotNull(actual);
        context.Assert.AreEqual(2, actual.Id);
        context.Assert.IsTrue(actual.IsNew);
        context.Assert.IsTrue(actual.IsDirty);
        context.Assert.IsFalse(actual.IsDeleted);
        context.Assert.AreEqual("user state", e.UserState.ToString());
        context.Assert.Success();
      }, userState);

      context.Complete();
    }

    [TestMethod]
    public void TestCreateNewRemoteProxyWithoutUserState()
    {
      Csla.DataPortal.ProxyTypeName = "Csla.DataPortalClient.WcfProxy, Csla";

      UnitTestContext context = GetContext();
      DataPortal.BeginCreate<Customer>(new SingleCriteria<Customer, int>(2), (o, e) =>
      {
        Customer actual = e.Object;
        context.Assert.IsNull(e.Error);
        context.Assert.IsNotNull(actual);
        context.Assert.AreEqual(2, actual.Id);
        context.Assert.IsTrue(actual.IsNew);
        context.Assert.IsTrue(actual.IsDirty);
        context.Assert.IsFalse(actual.IsDeleted);
        context.Assert.IsNull(e.UserState);
        context.Assert.Success();
      });

      context.Complete();
    }

    [TestMethod]
    public void TestUpdateRemoteProxyWithUserState()
    {
      Csla.DataPortal.ProxyTypeName = "Csla.DataPortalClient.WcfProxy, Csla";

      UnitTestContext context = GetContext();
      object userState = "user state";
      DataPortal.BeginCreate<Customer>(new SingleCriteria<Customer, int>(2), (o, e) =>
      {
        Customer actual = e.Object;
        actual.Name = "new name";
        DataPortal.BeginUpdate<Customer>(actual, (o1, e1) =>
          {
            context.Assert.IsNull(e.Error);
            context.Assert.IsNotNull(actual);
            context.Assert.AreEqual(2, actual.Id);
            context.Assert.IsTrue(actual.IsNew);
            context.Assert.IsTrue(actual.IsDirty);
            context.Assert.IsFalse(actual.IsDeleted);
            context.Assert.AreEqual("user state", e.UserState.ToString());
            context.Assert.Success();
          }, userState);

      }, userState);

      context.Complete();
    }

    [TestMethod]
    public void TestUpdateRemoteProxyWithoutUserState()
    {
      Csla.DataPortal.ProxyTypeName = "Csla.DataPortalClient.WcfProxy, Csla";

      UnitTestContext context = GetContext();
      DataPortal.BeginCreate<Customer>(new SingleCriteria<Customer, int>(2), (o, e) =>
      {
        Customer actual = e.Object;
        actual.Name = "new name";
        DataPortal.BeginUpdate<Customer>(actual, (o1, e1) =>
        {
          context.Assert.IsNull(e.Error);
          context.Assert.IsNotNull(actual);
          context.Assert.AreEqual(2, actual.Id);
          context.Assert.IsTrue(actual.IsNew);
          context.Assert.IsTrue(actual.IsDirty);
          context.Assert.IsFalse(actual.IsDeleted);
          context.Assert.IsNull(e.UserState);
          context.Assert.Success();
        });

      });

      context.Complete();
    }

    [TestMethod]
    public void TestDeleteRemoteProxyWithUserState()
    {
      Csla.DataPortal.ProxyTypeName = "Csla.DataPortalClient.WcfProxy, Csla";
      Csla.ApplicationContext.GlobalContext.Clear();
      UnitTestContext context = GetContext();
      object userState = "user state";
      DataPortal.BeginFetch<Customer>(new SingleCriteria<Customer, int>(2), (o, e) =>
      {
        Customer actual = e.Object;
        DataPortal.BeginDelete<Customer>(new SingleCriteria<Customer, int>(actual.Id), (o1, e1) =>
        {
          context.Assert.IsNull(e.Error);
          context.Assert.AreEqual("user state", e.UserState.ToString());
          context.Assert.Success();
        }, userState);

      }, userState);

      context.Complete();
    }

    [TestMethod]
    public void TestDeleteRemoteProxyWithoutUserState()
    {
      Csla.DataPortal.ProxyTypeName = "Csla.DataPortalClient.WcfProxy, Csla";
      Csla.ApplicationContext.GlobalContext.Clear();
      UnitTestContext context = GetContext();
      DataPortal.BeginFetch<Customer>(new SingleCriteria<Customer, int>(2), (o, e) =>
      {
        Customer actual = e.Object;
        DataPortal.BeginDelete<Customer>(new SingleCriteria<Customer, int>(actual.Id), (o1, e1) =>
        {
          context.Assert.IsNull(e.Error);
          context.Assert.IsNull(e.UserState);
          context.Assert.Success();
        });

      });

      context.Complete();
    }

    [TestMethod]
    public void TestFetchRemoteProxyWithUserState()
    {
      Csla.DataPortal.ProxyTypeName = "Csla.DataPortalClient.WcfProxy, Csla";
      Csla.ApplicationContext.GlobalContext.Clear();
      UnitTestContext context = GetContext();
      object userState = "user state";
      DataPortal.BeginFetch<Customer>(new SingleCriteria<Customer, int>(2), (o, e) =>
      {
        context.Assert.IsNull(e.Error);
        context.Assert.IsNotNull(e.Object);
        context.Assert.AreEqual(2, e.Object.Id);
        context.Assert.AreEqual("user state", e.UserState.ToString());
        context.Assert.Success();

      }, userState);

      context.Complete();
    }

    [TestMethod]
    public void TestFetchRemoteProxyWithoutUserState()
    {
      Csla.DataPortal.ProxyTypeName = "Csla.DataPortalClient.WcfProxy, Csla";
      Csla.ApplicationContext.GlobalContext.Clear();
      UnitTestContext context = GetContext();
      DataPortal.BeginFetch<Customer>(new SingleCriteria<Customer, int>(2), (o, e) =>
      {
        context.Assert.IsNull(e.Error);
        context.Assert.IsNotNull(e.Object);
        context.Assert.AreEqual(2, e.Object.Id);
        context.Assert.IsNull(e.UserState);
        context.Assert.Success();

      });

      context.Complete();
    }

    [TestMethod]
    public void TestSaveRemoteProxyWithUserState()
    {
      Csla.DataPortal.ProxyTypeName = "Csla.DataPortalClient.WcfProxy, Csla";

      UnitTestContext context = GetContext();
      object userState = "user state";
      DataPortal.BeginCreate<Customer>(new SingleCriteria<Customer, int>(2), (o, e) =>
      {
        Customer actual = e.Object;
        actual.Name = "new name";
        actual.BeginSave((o1, e1) =>
        {
          context.Assert.IsNull(e.Error);
          context.Assert.IsNotNull(actual);
          context.Assert.AreEqual(2, ((Customer)e1.NewObject).Id);
          context.Assert.IsFalse(((Customer)e1.NewObject).IsNew);
          context.Assert.IsFalse(((Customer)e1.NewObject).IsDirty);
          context.Assert.IsFalse(((Customer)e1.NewObject).IsDeleted);
          context.Assert.AreEqual("user state", e.UserState.ToString());
          context.Assert.Success();
        }, userState);

      }, userState);

      context.Complete();
    }

    [TestMethod]
    public void TestSaveRemoteProxyWithoutUserState()
    {
      Csla.DataPortal.ProxyTypeName = "Csla.DataPortalClient.WcfProxy, Csla";

      UnitTestContext context = GetContext();
      DataPortal.BeginCreate<Customer>(new SingleCriteria<Customer, int>(2), (o, e) =>
      {
        Customer actual = e.Object;
        actual.Name = "new name";
        actual.BeginSave((o1, e1) =>
        {
          context.Assert.IsNull(e.Error);
          context.Assert.IsNotNull(actual);
          context.Assert.AreEqual(2, ((Customer)e1.NewObject).Id);
          context.Assert.IsFalse(((Customer)e1.NewObject).IsNew);
          context.Assert.IsFalse(((Customer)e1.NewObject).IsDirty);
          context.Assert.IsFalse(((Customer)e1.NewObject).IsDeleted);
          context.Assert.IsNull(e.UserState);
          context.Assert.Success();
        });

      });

      context.Complete();
    }
  }
}