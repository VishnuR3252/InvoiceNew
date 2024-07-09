<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Sample.Test.WebForm1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sample Form</title>

</head>
<body>
    <form id="form1" runat="server" style="background-color:azure">
        <%--<div>
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            <br />
            <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
            <br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
        </div>--%>
        <div class="content">
            <div class="MainContent"> 
                <div class="row">
                    <div class="col-md-10 header animated bounceIn" style="z-index: 1">

                        <h2>Billing System</h2>


                    </div>


                </div> 
                <%-- nav bar tab --%>
                
              <br/>
                <div class="tab-content">
                      <%-- NormalRelease --%> 
                    
                        <div class="form-group">
                            

                                <label class="col-md-2 cntr-text">Select Customer:<span class="required text-danger">*</span></label>
                                 <asp:DropDownList ID="DropDownList1" runat="server" style="width: 15%;">
                                     <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                 </asp:DropDownList>&nbsp&nbsp
                                
                            

                                <label class="col-md-2 cntr-text">Select Product:<span class="required text-danger">*</span> </label>
                                <asp:DropDownList ID="DropDownList2" runat="server" style="width: 15%;">
                                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                </asp:DropDownList>

                            
                            </div><br />
                        <div class="form-group">
                            
                                <label id="T1" class="col-md-2 cntr-text">Quantity: </label>
                                <asp:TextBox ID="TextBox1" runat="server" style="width: 15%;"></asp:TextBox>&nbsp&nbsp

                             
                                <label id="T2" class="col-md-2 cntr-text">Tax: </label>
                                <asp:TextBox ID="TextBox2" runat="server" style="width: 15%;" ReadOnly="True">15</asp:TextBox>

                                 
                            <label id="T3" class="col-md-2 cntr-text">Price: </label>

                                <asp:TextBox ID="TextBox3" runat="server" style="width: 15%;"></asp:TextBox>

                            
                            </div><br />
                            
                        
                            

                        

                </div><br />



                        
                            <div class="col-md-6">

                                
                                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
                                <br />
                                <br />
                                
                                <asp:Button ID="Button2" runat="server" Text="Clear" OnClick="Button2_Click" />
                                
                            </div>
                        

                        </div>
                       
                        
                    </div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"></asp:GridView>

                    <%-- NormalRelease --%>
                    




                    <%-- Body End --%>
                
        <p>
            <asp:Label ID="Label1" runat="server" Text="Untaxed Amount"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Tax 15%"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label3" runat="server" Text="Total"></asp:Label>
        </p>
                
    </form>
</body>
</html>