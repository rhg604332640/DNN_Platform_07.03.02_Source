﻿#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2014
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
#region Usings

using System;

using DotNetNuke.Common.Internal;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Services.Search.Entities;

#endregion

namespace DotNetNuke.Services.Search.Controllers
{
    /// <summary>
    /// Search Result Controller for Tab Indexer
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class TabResultController : BaseResultController
    {
        #region Abstract Class Implmentation

        public override bool HasViewPermission(SearchResult searchResult)
        {
            var viewable = true;

            if (searchResult.TabId > 0)
            {
                var tab = TabController.Instance.GetTab(searchResult.TabId, searchResult.PortalId, false);
                viewable = tab != null && !tab.IsDeleted && TabPermissionController.CanViewPage(tab);
            }

            return viewable;
        }
        
        public override string GetDocUrl(SearchResult searchResult)
        {
            var url = Localization.Localization.GetString("SEARCH_NoLink");

            var tab = TabController.Instance.GetTab(searchResult.TabId, searchResult.PortalId, false);
            if (TabPermissionController.CanViewPage(tab))
            {
                url = TestableGlobals.Instance.NavigateURL(searchResult.TabId, string.Empty, searchResult.QueryString);
            }
            
            return url;
        }

        #endregion
    }
}
