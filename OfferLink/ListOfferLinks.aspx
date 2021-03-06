﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master/AdminMasterPage.Master"
    CodeBehind="ListOfferLinks.aspx.cs" Inherits="offerlinkmanageradmin.OfferLink.ListOfferLinks" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphRightPanel" runat="server">
    <html>
    <head>
        <title>Promotional Links</title>
        <link href="../css/admincp.css" type="text/css" rel="stylesheet">
        <script src="../scripts/jquery-1.4.1.js" type="text/javascript"></script>
        <style type="text/css">
            .ed
            {
                border: 1px solid #ccc;
                line-height: 11px;
                min-height: 16px;
                display: inline-block;
                padding: 0 15px 0 0;
                min-width: 200px;
            }
            .controlbtn
            {
                position: static;
            }
            .controlbtn.stick
            {
                position: fixed;
                top: 0;
                left: 209px;
                background: #fff;
                border-bottom: 1px solid gray;
            }
        </style>
        <style>
            .maintable{ border-collapse: collapse;}
            .maintable tr td{border: 1px solid #999999;}
        </style>
        <script type="text/javascript">
            var oldvalue = "";

            $(function () {
                var scrtop = $('.controlbtn').offset().top;
                var wt = $('.controlbtn').width();
                $('.controlbtn').width(wt);
                $(window).scroll(function () {
                    var wintop = $(window).scrollTop();
                    if (wintop >= scrtop) {
                        $('.controlbtn').addClass('stick');
                    } else {
                        $('.controlbtn').removeClass('stick');
                    }
                })

                dataEdit = function () {
                    if ($('[name^="fcheck"]:checked').length > 0) {
                        $('[name^="fcheck"]:checked').each(function () {
                            var id = $(this).val();
                            var el = $(this).parents('tr').eq(0);
                            el.find('#' + id + '_a').attr('contenteditable', true).addClass('ed');
                            el.find('#' + id + '_divck').attr('contenteditable', true).addClass('ed');
                            el.find('#' + id + '_divname').attr('contenteditable', true).addClass('ed');
                            el.find('#' + id + '_divbita').attr('contenteditable', true).addClass('ed');
                            el.find('#' + id + '_divbitylrel').attr('contenteditable', true).addClass('ed');
                            oldvalue += el.find('#' + id + '_div').text() + ',';

                        });
                        $('#save').show(); $('#edit').hide(); $('#edit').hide(); $('#cancel').show();

                    }
                    else {
                        $('#save').hide(); $('#edit').show(); $('#edit').show(); $('#cancel').hide();
                    }
                    oldvalue = oldvalue.substring(0, oldvalue.length - 1);

                }
                dataSave = function () {
                    var count = 0;
                    var icount = 0;
                    var stroldvalue = oldvalue.split(',');
                    if ($('[name^="fcheck"]:checked').length > 0) {
                        $('#loader').css({ display: 'inline-block' });
                        count = $('[name^="fcheck"]:checked').length;
                        var toSave = "";
                        $('[name^="fcheck"]:checked').each(function (index) {
                            var id = $(this).val();
                            var el = $(this).parents('tr').eq(0);
                            var link = el.find('#' + id + '_a').text();
                            var url = el.find('#' + id + '_divck').text();
                            var name = el.find('#' + id + '_divname').text();
                            var bitlyurl = el.find('#' + id + '_divbita').text();
                            var bityrel = el.find('#' + id + '_divbitylrel').text();
                            var _site = $("#" + id).val();
                            //alert(_site);
                            el.find('#' + id + '_a').attr('contenteditable', false).removeClass('ed');
                            el.find('#' + id + '_divck').attr('contenteditable', false).removeClass('ed');
                            el.find('#' + id + '_divname').attr('contenteditable', false).removeClass('ed');
                            el.find('#' + id + '_divbita').attr('contenteditable', false).removeClass('ed');
                            el.find('#' + id + '_divbitylrel').attr('contenteditable', false).removeClass('ed');
                            if (index == 0) {
                                toSave += id + '||' + link + "||" + url + "||" + name + "||" + bitlyurl + "||" + bityrel;
                                //alert(toSave);

                                Save(id, link, url, name, bitlyurl, stroldvalue[index], bityrel);
                                //alert(bitlyurl);
                            }
                            else {
                                toSave += '|||' + id + '||' + link + "||" + url + "||" + name + "||" + bitlyurl + "||" + bityrel;
                                //alert(toSave);
                                //alert(bitlyurl);

                                Save(id, link, url, name, bitlyurl, stroldvalue[index], bityrel);

                            }

                            oldvalue = "";

                        })
                        //$('#loader').hide();
                        $('#save').hide(); $('#edit').show(); $('#cancel').hide();
                        $('[name^="fcheck"], [name="selectall"]').attr('checked', false);

                    }
                    function filltags_Callback(response) {
                        icount++;
                        if (icount == count) {
                            $('#loader').hide();
                        }
                    }
                    function Save(linkid, linkurl, cookie_data, link_name, bitlyurl, _oldvalue, _bitlyrel) {
                        var url = "<%= BaseUrl %>AjaxOfferLink.aspx";
                        $.ajax({
                            type: "POST",
                            url: url,
                            cache: false,
                            async: false,
                            data: { type: 'offerlink', linkid: linkid, linkurl: linkurl, cookieuri: cookie_data, linkname: link_name, shortenurl: bitlyurl, oldvalue: _oldvalue, bitlyrel: _bitlyrel },
                            success: function (msg) {

                                filltags_Callback(msg);

                            },
                            error: function (request, err) {
                            }
                        });
                    }
                }



                dataCancel = function () {
                    if ($('[name^="fcheck"]:checked').length > 0) {

                        var toSave = "";
                        $('[name^="fcheck"]:checked').each(function (index) {
                            var id = $(this).val();
                            var el = $(this).parents('tr').eq(0);
                            var link = el.find('#' + id + '_a').text();
                            var url = el.find('#' + id + '_divck').text();
                            var name = el.find('#' + id + '_divname').text();
                            el.find('#' + id + '_a').attr('contenteditable', false).removeClass('ed');
                            el.find('#' + id + '_divck').attr('contenteditable', false).removeClass('ed');
                            el.find('#' + id + '_divname').attr('contenteditable', false).removeClass('ed');
                            el.find('#' + id + '_divbita').attr('contenteditable', false).removeClass('ed');
                            el.find('#' + id + '_divbitylrel').attr('contenteditable', false).removeClass('ed');
                        })
                    }
                    $('#save').hide(); $('#edit').show(); $('#cancel').hide();
                    oldvalue = "";
                }


                $('#edit').click(function () { dataEdit(); })
                $('#save').click(function () { dataSave(); })
                $('#cancel').click(function () { dataCancel(); })

            })

        </script>
    </head>
    <body bottommargin="0" bgcolor="#eeeeee" leftmargin="0" topmargin="0" rightmargin="0">
        <table height="100%" cellspacing="1" cellpadding="0" width="100%" align="center"
            border="0">
            <tr height="20">
                <td class="header" align="left" width="82%" colspan="2">
                    &gt;&gt;
                    <asp:Literal ID="ltheader" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" width="874" bgcolor="#ffffff">
                    <form id="form1" method="post" runat="server">
                    <table cellspacing="1" cellpadding="0" width="100%" border="0" class="maintable">
                        <tr>
                            <td class="text" align="left" bgcolor="#ffffff" colspan="11" style='border-bottom-style: hidden;'>
                                <asp:Literal ID="ltpaging" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td align="left" bgcolor="#ffffff" colspan="11" style='border-bottom-style: hidden;'>
                                <div class="controlbtn">
                                    <input id="cancel" type="button" class="buttontext" value="Cancel" style="display: none;" />
                                    <input type="button" class='buttontext' id="edit" name="addlinkmanager" value="Edit"
                                        style='display: none;' />
                                    <input type="button" class='buttontext' id="save" name="addlinkmanager" value="Save"
                                        style="display: none;" />
                                    &nbsp;
                                    <input class="buttontext" id="activate" onclick="javascript:apporveallselchk();"
                                        type="button" value="Activate" name="activatecat">
                                    <input class="buttontext" id="deactivate" onclick="javascript:deapporveallselchk();"
                                        type="button" value="Deactivate" name="deactivatecat">
                                    <input class="buttontext" id="deletefooter" onclick="javascript:deleteallselchk();"
                                        type="button" value="Delete" name="delete">
                                    <input class="buttontext" id="addfooter1" onclick="ShowAdd();" type="button" value="Add Link"
                                        name="adcat" />
                                     <input class="buttontext" id="btnaddwh" onclick="window.location.href='AddEditWHLink.aspx?linktype=w'" type="button" value="Add WH Link"
                                        name="adcat" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="text" align="left" bgcolor="#ffffff" colspan="11">
                                Region
                                <asp:DropDownList ID="ddlregion" runat="server" CssClass="text" AutoPostBack="true">                                   
                                    <asp:ListItem Text="AU" Value="AU"></asp:ListItem>
                                    <asp:ListItem Text="UK" Value="GB" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                Search
                                <asp:TextBox ID="txtsearch" runat="server" size="50" CssClass="text"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnsearch" class="buttontext" runat="server" Text="Search">
                                </asp:Button>
                                &nbsp;<input id="btnClear" type="button" class="buttontext" value="Clear" onclick="Clear();" />
                                &nbsp; Sort by date:
                                <asp:DropDownList ID="ddlsort" runat="server" CssClass="text" AutoPostBack="True">
                                    <asp:ListItem Value="0" Selected="True">-select-</asp:ListItem>
                                    <asp:ListItem Value="addasc">addedon ascending</asp:ListItem>
                                    <asp:ListItem Value="adddesc">addedon descending</asp:ListItem>
                                    <asp:ListItem Value="modasc">modifiedon ascending</asp:ListItem>
                                    <asp:ListItem Value="moddesc">modifiedon descending</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                        </tr>
                        <tr bgcolor="#eeeeee" height="20">
                            <td class="headings" align="center" width="40px">
                                Check All<br />
                                <input onclick="checkBoxes()" type="checkbox" name="selectall" />
                            </td>
                            <td class="headings" align="left" width="300px">
                                Link Name
                            </td>
                            <td class="headings" align="left" width="50px">
                                CM Link Number
                            </td>
                            <td class="headings" align="left" width="200px">
                                Link
                            </td>
                            <td class="headings" align="left" width="200px">
                                Bitly Url
                            </td>
                            <td class="headings" align="left" width="200px">
                                Bitly Url Relation
                            </td>
                            <td class="headings" align="left" width="100px">
                                Addded by/<br />
                                Modified by /<br />
                                Deleted by
                            </td>
                            <td class="headings" align="center" width="70px">
                                Added On/<br />
                                Modified On
                            </td>
                            <td class="headings" align="center" width="70px">
                                Expire Date
                            </td>
                            <td class="headings" align="center" width="60px">
                                Status
                            </td>
                            <td class="headings" align="left" width="300px">
                                View History
                            </td>
                        </tr>
                        <asp:Literal ID="ltlist" runat="server"></asp:Literal>
                    </table>
                    </form>
                </td>
            </tr>
        </table>
        <style type="text/css">
            #cropPopup
            {
                width: 600px;
                position: absolute;
                top: 100px;
                left: 50%;
                -webkit-transform: translateX(-50%);
                -moz-transform: translateX(-50%);
                transform: translateX(-50%);
                background: #fff;
                padding: 10px;
                border-radius: 2px;
                color: #808080;
                font-size: 14px;
                box-shadow: 0px 1px 3px 1px rgba(0, 0, 0, 0.6);
                -webkit-box-shadow: 0px 1px 3px 1px rgba(0, 0, 0, 0.6);
                -moz-box-shadow: 0px 1px 3px 1px rgba(0, 0, 0, 0.6);
                -webkit-user-select: none;
                -khtml-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
                cursor: default;
                visibility: hidden;
                opacity: 0;
                transition: all 0.3s;
                -webkit-transition: all 0.3s;
                -moz-transition: all 0.3s;
                z-index: 99;
            }
            #cropPopup.template
            {
                width: calc(100vw - 40px);
                width: -webkit-calc(100vw - 40px);
                top: 5px;
                padding-top: 30px;
            }
            #cropPopupOver
            {
                width: 100%;
                height: 100%;
                position: fixed;
                top: 0;
                left: 0;
                background: rgba(255,255,255,0.5);
                transition: all 0.3s;
                -webkit-transition: all 0.3s;
                -moz-transition: all 0.3s;
                visibility: hidden;
                opacity: 0;
                z-index: 98;
            }
            body.cPop
            {
                overflow: hidden;
            }
            body.cPop #cropPopup
            {
                visibility: visible;
                opacity: 1;
                position: fixed;
                height: 400px;
                width: 800px;
            }
            body.cPop #cropPopupOver
            {
                visibility: visible;
                opacity: 1;
            }
            #cropPopup i.removeCrop
            {
                width: 14px;
                height: 21px;
                position: absolute;
                top: -4px;
                right: 15px;
                cursor: pointer;
                font-style: normal;
                font-size: 24px;
            }
            #cropPopup.template i.removeCrop
            {
                top: 0;
            }
            #cropPopup i.removeCrop::before
            {
                content: '\2715';
            }
        </style>
        <div id="cropPopup">
        </div>
        <div id="cropPopupOver">
        </div>
        <script language="javascript">





            var maineditlink = "ListOfferLinkHistory.aspx?linkid=";
            function closePop(e) { if (e.data == 'closewin') { document.body.setAttribute('class', '') } }
            var events = window.addEventListener ? 'addEventListener' : 'attachEvent',
		listner = window[events],
		message = events == 'addEventListener' ? 'message' : 'onmessage';
            listner(message, closePop);
            function closePOP() { document.body.setAttribute('class', '') }



            $('.editlink').bind('click', function (e) {
                e.preventDefault(); e.stopPropagation();
                window['activerow'] = $(this).parents('tr');
                var href = maineditlink + $(this).attr('data-linkid');

                var frame = document.createElement('iframe');
                frame.id = 'cropPopframe';
                frame.src = href;
                frame.setAttribute('frameborder', 0);
                // frame.setAttribute('scrolling', 'no');
                frame.setAttribute('width', '100%');
                frame.setAttribute('height', '400');
                var cl = document.createElement('i');
                cl.className = 'removeCrop';
                $('#cropPopup').html(frame);
                $('#cropPopup').addClass('editlink')
                $('#cropPopup').append(cl);
                $('body').addClass('cPop');
            })

            $(document).bind('click', '.removeCrop', function () { $('body').removeClass('cPop'); })

            window.addEventListener('message', function (e) {
                if (e.data) {
                    console.log(e.data);
                    var data = e.data, _name = data[1], _link = data[2], _cookieurl = data[3], _bitly = data[4];
                    var name = $('#' + e.data[0] + '_divname a'), link = $('#' + e.data[0] + '_a'), cookieurl = $('#' + e.data[0] + '_divck'), bitly = $('#' + e.data[0] + '_divbita');
                    name.html(_name), link.html(_link)[0].href = _link, cookieurl.html(_cookieurl), bitly.html(_bitly)[0].href = _bitly;
                    $('body').removeClass('cPop');
                }
            }, false)

        </script>
        <script language="javascript">


            function ConfirmDelete(id) {
                if (confirm("Are you sure want to delete selected record, action cannot be undone.")) {
                    window.location.href = 'ListOfferLinks.aspx?id=' + id + '&stat=del';
                }
            }
            function ShowAdd() {
                window.location.href = "AddEditOfferLink.aspx";
            }

            function Clear() {
                window.location.href = "ListOfferLinks.aspx?clear=clear";
            }

            var frm = document.getElementById("aspnetForm");

            function checkBoxes() {
                if (frm.selectall.checked == true) {
                    var emp = frm['fcheck[]'];
                    if (emp.length) {
                        for (i = 0; i < emp.length; i++) {
                            emp[i].checked = true;
                        }
                    }
                    else {
                        emp.checked = true;
                    }
                }
                else {
                    var emp = frm['fcheck[]'];
                    if (emp.length) {
                        for (i = 0; i < emp.length; i++) {
                            emp[i].checked = false;
                        }
                    }
                    else {
                        emp.checked = false;
                    }
                }
            }
            function disselect() {
                var emp = frm['fcheck[]'];
                if (emp.length) {
                    for (i = 0; i < emp.length; i++) {
                        if (emp[i].checked == false) {
                            frm.selectall.checked = false;
                        }
                    }
                }
                else {
                    if (emp.checked == false)
                        frm.selectall.checked = false;
                }
            }

            function apporveallselchk() {
                var retVal = "";

                var empls = frm['fcheck[]'];
                if (empls) {
                    if (empls.length) {
                        for (i = 0; i < empls.length; i++) {
                            if (empls[i].checked) {
                                if (retVal.length > 0) retVal += ",";
                                retVal += empls[i].value;
                            }
                        }
                    } else {
                        retVal += empls.value;
                    }
                }

                if (retVal.length > 0) {
                    window.location.href = '<asp:Literal id=ltbaseurl runat=server></asp:Literal>OfferLink/ListOfferLinks.aspx?siteid=<asp:Literal id=ltsiteid runat=server></asp:Literal>&aprids=' + retVal + '&re=Y';
                }
                else {
                    alert('Please check atleast one checkbox');
                }
            }

            function deapporveallselchk() {
                var retVal = "";

                var empls = frm['fcheck[]'];
                if (empls) {
                    if (empls.length) {
                        for (i = 0; i < empls.length; i++) {
                            if (empls[i].checked) {
                                if (retVal.length > 0) retVal += ",";
                                retVal += empls[i].value;
                            }
                        }
                    } else {
                        retVal += empls.value;
                    }
                }

                if (retVal.length > 0) {
                    window.location.href = '<asp:Literal id=ltbaseurl2 runat=server></asp:Literal>OfferLink/ListOfferLinks.aspx?siteid=<asp:Literal id=ltsiteid2 runat=server></asp:Literal>&aprids=' + retVal + '&re=N';
                }

                else {
                    alert('Please check atleast one checkbox');
                }
            }
            function deleteallselchk() {
                var retVal = "";
                var empls = frm['fcheck[]'];
                if (empls) {
                    if (empls.length) {
                        for (i = 0; i < empls.length; i++) {
                            if (empls[i].checked) {
                                if (retVal.length > 0) retVal += ",";
                                retVal += empls[i].value;
                            }
                        }
                    } else {
                        retVal += empls.value;
                    }
                }

                if (retVal.length > 0) {
                    if (confirm("Are you sure want to delete selected record, action cannot be undone.")) {
                        window.location.href = '<asp:Literal id=ltbaseurl1 runat=server></asp:Literal>OfferLink/ListOfferLinks.aspx?siteid=<asp:Literal id=ltsiteid1 runat=server></asp:Literal>&aprids=' + retVal + '&re=D';
                    }

                }
                else {
                    alert('Please check atleast one checkbox');
                }

            }

            $(function () {

                $(".linktd span.added").each(function (index) {
                    var id = $(this).attr('id');
                    var userid = $(this).attr('data-id');
                    fncallAddedBy(userid, id);


                });



                $(".linktd span.thread").each(function (index) {
                    var id = $(this).attr('id');
                    var deluser = $("span#" + id);
                    var deluserid = $(deluser).attr('id');
                    var strid = $(deluser).attr('data-id');
                    var strarry = strid.split('_');
                    if (strarry[1] != 0) {

                        fncallDeletedBy(strarry[1], deluserid);
                    }
                });


            });


            function fncallDeletedBy(_userid, _strid) {
                var url = "<%=BaseUrl%>AjaxOfferLink.aspx";

                $.ajax({
                    type: 'POST',
                    url: url,
                    cache: false,
                    data: { type: 'FNGETUSER', userid: _userid },
                    success: function (msg) {

                        $("#" + _strid).html('deleted by - ' + msg);
                    },
                    error: function (request, err) {

                    }
                });

            }



            function fncallAddedBy(_userid, _strid) {
                var url = "<%=BaseUrl%>AjaxOfferLink.aspx";

                $.ajax({
                    type: 'POST',
                    url: url,
                    cache: false,
                    data: { type: 'FNGETUSER', userid: _userid },
                    success: function (msg) {

                        $("#" + _strid).html(msg);
                    },
                    error: function (request, err) {

                    }
                });

            }
        </script>
    </body>
    </html>
</asp:Content>
