<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="registerCustomer.aspx.cs" Inherits="mainCoursework.registerCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<p>
		Register New Customer</p>
	<p>
		<asp:TextBox ID="usernameBox" runat="server" placeholder ="Username"></asp:TextBox>
	</p>
	<p>
		<asp:TextBox ID="passwordBox" runat="server" placeholder ="Password" TextMode="Password" OnTextChanged="passwordBox_TextChanged"></asp:TextBox>
		<asp:TextBox ID="confirmPasswordBox" runat="server" placeholder ="Confirm Password" TextMode="Password" OnTextChanged="confirmPasswordBox_TextChanged"></asp:TextBox>
		<asp:Label ID="passwordBoxReturn" runat="server" ForeColor="Red" Text=""></asp:Label>
	</p>
	<p>
		<asp:TextBox ID="forenameBox" runat="server" placeholder ="First Name(s)"></asp:TextBox>
		<asp:TextBox ID="surnameBox" runat="server" placeholder ="Last Name"></asp:TextBox>
	<p>
		<asp:TextBox ID="address1Box" runat="server" placeholder ="Address Line 1"></asp:TextBox>
	</p>
	<p>
		<asp:TextBox ID="address2Box" runat="server" placeholder ="Address Line 2"></asp:TextBox>
	</p>
	<p>
		<asp:TextBox ID="cityBox" runat="server" placeholder ="City/Town"></asp:TextBox>
	</p>
	<p>
		<asp:TextBox ID="postcodeBox" runat="server" placeholder ="Postcode"></asp:TextBox>
	</p>
	<asp:DropDownList ID="countryDropdown" runat="server">
		<asp:ListItem value="Not Given"> Please Select A Country</asp:ListItem>
		<asp:ListItem value="Afghanistan"> Afghanistan </asp:ListItem>
        <asp:ListItem value="Albania"> Albania </asp:ListItem>
        <asp:ListItem value="Algeria"> Algeria </asp:ListItem>
        <asp:ListItem value="American Samoa"> American Samoa </asp:ListItem>
        <asp:ListItem value="Andorra"> Andorra </asp:ListItem>
        <asp:ListItem value="Angola"> Angola </asp:ListItem>
        <asp:ListItem value="Anguilla"> Anguilla </asp:ListItem>
        <asp:ListItem value="Antigua and Barbuda"> Antigua and Barbuda </asp:ListItem>
        <asp:ListItem value="Argentina"> Argentina </asp:ListItem>
        <asp:ListItem value="Armenia"> Armenia </asp:ListItem>
        <asp:ListItem value="Aruba"> Aruba </asp:ListItem>
        <asp:ListItem value="Australia"> Australia </asp:ListItem>
        <asp:ListItem value="Austria"> Austria </asp:ListItem>
        <asp:ListItem value="Azerbaijan"> Azerbaijan </asp:ListItem>
        <asp:ListItem value="The Bahamas"> The Bahamas </asp:ListItem>
        <asp:ListItem value="Bahrain"> Bahrain </asp:ListItem>
        <asp:ListItem value="Bangladesh"> Bangladesh </asp:ListItem>
        <asp:ListItem value="Barbados"> Barbados </asp:ListItem>
        <asp:ListItem value="Belarus"> Belarus </asp:ListItem>
        <asp:ListItem value="Belgium"> Belgium </asp:ListItem>
        <asp:ListItem value="Belize"> Belize </asp:ListItem>
        <asp:ListItem value="Benin"> Benin </asp:ListItem>
        <asp:ListItem value="Bermuda"> Bermuda </asp:ListItem>
        <asp:ListItem value="Bhutan"> Bhutan </asp:ListItem>
        <asp:ListItem value="Bolivia"> Bolivia </asp:ListItem>
        <asp:ListItem value="Bosnia and Herzegovina"> Bosnia and Herzegovina </asp:ListItem>
        <asp:ListItem value="Botswana"> Botswana </asp:ListItem>
        <asp:ListItem value="Brazil"> Brazil </asp:ListItem>
        <asp:ListItem value="Brunei"> Brunei </asp:ListItem>
        <asp:ListItem value="Bulgaria"> Bulgaria </asp:ListItem>
        <asp:ListItem value="Burkina Faso"> Burkina Faso </asp:ListItem>
        <asp:ListItem value="Burundi"> Burundi </asp:ListItem>
        <asp:ListItem value="Cambodia"> Cambodia </asp:ListItem>
        <asp:ListItem value="Cameroon"> Cameroon </asp:ListItem>
        <asp:ListItem value="Canada"> Canada </asp:ListItem>
        <asp:ListItem value="Cape Verde"> Cape Verde </asp:ListItem>
        <asp:ListItem value="Cayman Islands"> Cayman Islands </asp:ListItem>
        <asp:ListItem value="Central African Republic"> Central African Republic </asp:ListItem>
        <asp:ListItem value="Chad"> Chad </asp:ListItem>
        <asp:ListItem value="Chile"> Chile </asp:ListItem>
        <asp:ListItem value="People 's Republic of China"> People 's Republic of China </asp:ListItem>
        <asp:ListItem value="Republic of China"> Republic of China </asp:ListItem>
        <asp:ListItem value="Christmas Island"> Christmas Island </asp:ListItem>
        <asp:ListItem value="Cocos(Keeling) Islands"> Cocos(Keeling) Islands </asp:ListItem>
        <asp:ListItem value="Colombia"> Colombia </asp:ListItem>
        <asp:ListItem value="Comoros"> Comoros </asp:ListItem>
        <asp:ListItem value="Congo"> Congo </asp:ListItem>
        <asp:ListItem value="Cook Islands"> Cook Islands </asp:ListItem>
        <asp:ListItem value="Costa Rica"> Costa Rica </asp:ListItem>
        <asp:ListItem value="Cote d'Ivoire"> Cote d'Ivoire </asp:ListItem>
        <asp:ListItem value="Croatia"> Croatia </asp:ListItem>
        <asp:ListItem value="Cuba"> Cuba </asp:ListItem>
        <asp:ListItem value="Cyprus"> Cyprus </asp:ListItem>
        <asp:ListItem value="Czech Republic"> Czech Republic </asp:ListItem>
        <asp:ListItem value="Denmark"> Denmark </asp:ListItem>
        <asp:ListItem value="Djibouti"> Djibouti </asp:ListItem>
        <asp:ListItem value="Dominica"> Dominica </asp:ListItem>
        <asp:ListItem value="Dominican Republic"> Dominican Republic </asp:ListItem>
        <asp:ListItem value="Ecuador"> Ecuador </asp:ListItem>
        <asp:ListItem value="Egypt"> Egypt </asp:ListItem>
        <asp:ListItem value="El Salvador"> El Salvador </asp:ListItem>
        <asp:ListItem value="Equatorial Guinea"> Equatorial Guinea </asp:ListItem>
        <asp:ListItem value="Eritrea"> Eritrea </asp:ListItem>
        <asp:ListItem value="Estonia"> Estonia </asp:ListItem>
        <asp:ListItem value="Ethiopia"> Ethiopia </asp:ListItem>
        <asp:ListItem value="Falkland Islands"> Falkland Islands </asp:ListItem>
        <asp:ListItem value="Faroe Islands"> Faroe Islands </asp:ListItem>
        <asp:ListItem value="Fiji"> Fiji </asp:ListItem>
        <asp:ListItem value="Finland"> Finland </asp:ListItem>
        <asp:ListItem value="France"> France </asp:ListItem>
        <asp:ListItem value="French Polynesia"> French Polynesia </asp:ListItem>
        <asp:ListItem value="Gabon"> Gabon </asp:ListItem>
        <asp:ListItem value="The Gambia"> The Gambia </asp:ListItem>
        <asp:ListItem value="Georgia"> Georgia </asp:ListItem>
        <asp:ListItem value="Germany"> Germany </asp:ListItem>
        <asp:ListItem value="Ghana"> Ghana </asp:ListItem>
        <asp:ListItem value="Gibraltar"> Gibraltar </asp:ListItem>
        <asp:ListItem value="Greece"> Greece </asp:ListItem>
        <asp:ListItem value="Greenland"> Greenland </asp:ListItem>
        <asp:ListItem value="Grenada"> Grenada </asp:ListItem>
        <asp:ListItem value="Guadeloupe"> Guadeloupe </asp:ListItem>
        <asp:ListItem value="Guam"> Guam </asp:ListItem>
        <asp:ListItem value="Guatemala"> Guatemala </asp:ListItem>
        <asp:ListItem value="Guernsey"> Guernsey </asp:ListItem>
        <asp:ListItem value="Guinea"> Guinea </asp:ListItem>
        <asp:ListItem value="Guinea - Bissau"> Guinea - Bissau </asp:ListItem>
        <asp:ListItem value="Guyana"> Guyana </asp:ListItem>
        <asp:ListItem value="Haiti"> Haiti </asp:ListItem>
        <asp:ListItem value="Honduras"> Honduras </asp:ListItem>
        <asp:ListItem value="Hong Kong"> Hong Kong </asp:ListItem>
        <asp:ListItem value="Hungary"> Hungary </asp:ListItem>
        <asp:ListItem value="Iceland"> Iceland </asp:ListItem>
        <asp:ListItem value="India"> India </asp:ListItem>
        <asp:ListItem value="Indonesia"> Indonesia </asp:ListItem>
        <asp:ListItem value="Iran"> Iran </asp:ListItem>
        <asp:ListItem value="Iraq"> Iraq </asp:ListItem>
        <asp:ListItem value="Ireland"> Ireland </asp:ListItem>
        <asp:ListItem value="Israel"> Israel </asp:ListItem>
        <asp:ListItem value="Italy"> Italy </asp:ListItem>
        <asp:ListItem value="Jamaica"> Jamaica </asp:ListItem>
        <asp:ListItem value="Japan"> Japan </asp:ListItem>
        <asp:ListItem value="Jersey"> Jersey </asp:ListItem>
        <asp:ListItem value="Jordan"> Jordan </asp:ListItem>
        <asp:ListItem value="Kazakhstan"> Kazakhstan </asp:ListItem>
        <asp:ListItem value="Kenya"> Kenya </asp:ListItem>
        <asp:ListItem value="Kiribati"> Kiribati </asp:ListItem>
        <asp:ListItem value="North Korea"> North Korea </asp:ListItem>
        <asp:ListItem value="South Korea"> South Korea </asp:ListItem>
        <asp:ListItem value="Kosovo"> Kosovo </asp:ListItem>
        <asp:ListItem value="Kuwait"> Kuwait </asp:ListItem>
        <asp:ListItem value="Kyrgyzstan"> Kyrgyzstan </asp:ListItem>
        <asp:ListItem value="Laos"> Laos </asp:ListItem>
        <asp:ListItem value="Latvia"> Latvia </asp:ListItem>
        <asp:ListItem value="Lebanon"> Lebanon </asp:ListItem>
        <asp:ListItem value="Lesotho"> Lesotho </asp:ListItem>
        <asp:ListItem value="Liberia"> Liberia </asp:ListItem>
        <asp:ListItem value="Libya"> Libya </asp:ListItem>
        <asp:ListItem value="Liechtenstein"> Liechtenstein </asp:ListItem>
        <asp:ListItem value="Lithuania"> Lithuania </asp:ListItem>
        <asp:ListItem value="Luxembourg"> Luxembourg </asp:ListItem>
        <asp:ListItem value="Macau"> Macau </asp:ListItem>
        <asp:ListItem value="Macedonia"> Macedonia </asp:ListItem>
        <asp:ListItem value="Madagascar"> Madagascar </asp:ListItem>
        <asp:ListItem value="Malawi"> Malawi </asp:ListItem>
        <asp:ListItem value="Malaysia"> Malaysia </asp:ListItem>
        <asp:ListItem value="Maldives"> Maldives </asp:ListItem>
        <asp:ListItem value="Mali"> Mali </asp:ListItem>
        <asp:ListItem value="Malta"> Malta </asp:ListItem>
        <asp:ListItem value="Marshall Islands"> Marshall Islands </asp:ListItem>
        <asp:ListItem value="Martinique"> Martinique </asp:ListItem>
        <asp:ListItem value="Mauritania"> Mauritania </asp:ListItem>
        <asp:ListItem value="Mauritius"> Mauritius </asp:ListItem>
        <asp:ListItem value="Mayotte"> Mayotte </asp:ListItem>
        <asp:ListItem value="Mexico"> Mexico </asp:ListItem>
        <asp:ListItem value="Micronesia"> Micronesia </asp:ListItem>
        <asp:ListItem value="Moldova"> Moldova </asp:ListItem>
        <asp:ListItem value="Monaco"> Monaco </asp:ListItem>
        <asp:ListItem value="Mongolia"> Mongolia </asp:ListItem>
        <asp:ListItem value="Montenegro"> Montenegro </asp:ListItem>
        <asp:ListItem value="Montserrat"> Montserrat </asp:ListItem>
        <asp:ListItem value="Morocco"> Morocco </asp:ListItem>
        <asp:ListItem value="Mozambique"> Mozambique </asp:ListItem>
        <asp:ListItem value="Myanmar"> Myanmar </asp:ListItem>
        <asp:ListItem value="Nagorno - Karabakh"> Nagorno - Karabakh </asp:ListItem>
        <asp:ListItem value="Namibia"> Namibia </asp:ListItem>
        <asp:ListItem value="Nauru"> Nauru </asp:ListItem>
        <asp:ListItem value="Nepal"> Nepal </asp:ListItem>
        <asp:ListItem value="Netherlands"> Netherlands </asp:ListItem>
        <asp:ListItem value="Netherlands Antilles"> Netherlands Antilles </asp:ListItem>
        <asp:ListItem value="New Caledonia"> New Caledonia </asp:ListItem>
        <asp:ListItem value="New Zealand"> New Zealand </asp:ListItem>
        <asp:ListItem value="Nicaragua"> Nicaragua </asp:ListItem>
        <asp:ListItem value="Niger"> Niger </asp:ListItem>
        <asp:ListItem value="Nigeria"> Nigeria </asp:ListItem>
        <asp:ListItem value="Niue"> Niue </asp:ListItem>
        <asp:ListItem value="Norfolk Island"> Norfolk Island </asp:ListItem>
        <asp:ListItem value="Turkish Republic of Northern Cyprus"> Turkish Republic of Northern Cyprus </asp:ListItem>
        <asp:ListItem value="Northern Mariana"> Northern Mariana </asp:ListItem>
        <asp:ListItem value="Norway"> Norway </asp:ListItem>
        <asp:ListItem value="Oman"> Oman </asp:ListItem>
        <asp:ListItem value="Pakistan"> Pakistan </asp:ListItem>
        <asp:ListItem value="Palau"> Palau </asp:ListItem>
        <asp:ListItem value="Palestine"> Palestine </asp:ListItem>
        <asp:ListItem value="Panama"> Panama </asp:ListItem>
        <asp:ListItem value="Papua New Guinea"> Papua New Guinea </asp:ListItem>
        <asp:ListItem value="Paraguay"> Paraguay </asp:ListItem>
        <asp:ListItem value="Peru"> Peru </asp:ListItem>
        <asp:ListItem value="Philippines"> Philippines </asp:ListItem>
        <asp:ListItem value="Pitcairn Islands"> Pitcairn Islands </asp:ListItem>
        <asp:ListItem value="Poland"> Poland </asp:ListItem>
        <asp:ListItem value="Portugal"> Portugal </asp:ListItem>
        <asp:ListItem value="Puerto Rico"> Puerto Rico </asp:ListItem>
        <asp:ListItem value="Qatar"> Qatar </asp:ListItem>
        <asp:ListItem value="Romania"> Romania </asp:ListItem>
        <asp:ListItem value="Russia"> Russia </asp:ListItem>
        <asp:ListItem value="Rwanda"> Rwanda </asp:ListItem>
        <asp:ListItem value="Saint Barthelemy"> Saint Barthelemy </asp:ListItem>
        <asp:ListItem value="Saint Helena"> Saint Helena </asp:ListItem>
        <asp:ListItem value="Saint Kitts and Nevis"> Saint Kitts and Nevis </asp:ListItem>
        <asp:ListItem value="Saint Lucia"> Saint Lucia </asp:ListItem>
        <asp:ListItem value="Saint Martin"> Saint Martin </asp:ListItem>
        <asp:ListItem value="Saint Pierre and Miquelon"> Saint Pierre and Miquelon </asp:ListItem>
        <asp:ListItem value="Saint Vincent and the Grenadines"> Saint Vincent and the Grenadines </asp:ListItem>
        <asp:ListItem value="Samoa"> Samoa </asp:ListItem>
        <asp:ListItem value="San Marino"> San Marino </asp:ListItem>
        <asp:ListItem value="Sao Tome and Principe"> Sao Tome and Principe </asp:ListItem>
        <asp:ListItem value="Saudi Arabia"> Saudi Arabia </asp:ListItem>
        <asp:ListItem value="Senegal"> Senegal </asp:ListItem>
        <asp:ListItem value="Serbia"> Serbia </asp:ListItem>
        <asp:ListItem value="Seychelles"> Seychelles </asp:ListItem>
        <asp:ListItem value="Sierra Leone"> Sierra Leone </asp:ListItem>
        <asp:ListItem value="Singapore"> Singapore </asp:ListItem>
        <asp:ListItem value="Slovakia"> Slovakia </asp:ListItem>
        <asp:ListItem value="Slovenia"> Slovenia </asp:ListItem>
        <asp:ListItem value="Solomon Islands"> Solomon Islands </asp:ListItem>
        <asp:ListItem value="Somalia"> Somalia </asp:ListItem>
        <asp:ListItem value="Somaliland"> Somaliland </asp:ListItem>
        <asp:ListItem value="South Africa"> South Africa </asp:ListItem>
        <asp:ListItem value="South Ossetia"> South Ossetia </asp:ListItem>
        <asp:ListItem value="Spain"> Spain </asp:ListItem>
        <asp:ListItem value="Sri Lanka"> Sri Lanka </asp:ListItem>
        <asp:ListItem value="Sudan"> Sudan </asp:ListItem>
        <asp:ListItem value="Suriname"> Suriname </asp:ListItem>
        <asp:ListItem value="Svalbard"> Svalbard </asp:ListItem>
        <asp:ListItem value="Swaziland"> Swaziland </asp:ListItem>
        <asp:ListItem value="Sweden"> Sweden </asp:ListItem>
        <asp:ListItem value="Switzerland"> Switzerland </asp:ListItem>
        <asp:ListItem value="Syria"> Syria </asp:ListItem>
        <asp:ListItem value="Taiwan"> Taiwan </asp:ListItem>
        <asp:ListItem value="Tajikistan"> Tajikistan </asp:ListItem>
        <asp:ListItem value="Tanzania"> Tanzania </asp:ListItem>
        <asp:ListItem value="Thailand"> Thailand </asp:ListItem>
        <asp:ListItem value="Timor - Leste"> Timor - Leste </asp:ListItem>
        <asp:ListItem value="Togo"> Togo </asp:ListItem>
        <asp:ListItem value="Tokelau"> Tokelau </asp:ListItem>
        <asp:ListItem value="Tonga"> Tonga </asp:ListItem>
        <asp:ListItem value="Transnistria Pridnestrovie"> Transnistria Pridnestrovie </asp:ListItem>
        <asp:ListItem value="Trinidad and Tobago"> Trinidad and Tobago </asp:ListItem>
        <asp:ListItem value="Tristan da Cunha"> Tristan da Cunha </asp:ListItem>
        <asp:ListItem value="Tunisia"> Tunisia </asp:ListItem>
        <asp:ListItem value="Turkey"> Turkey </asp:ListItem>
        <asp:ListItem value="Turkmenistan"> Turkmenistan </asp:ListItem>
        <asp:ListItem value="Turks and Caicos Islands"> Turks and Caicos Islands </asp:ListItem>
        <asp:ListItem value="Tuvalu"> Tuvalu </asp:ListItem>
        <asp:ListItem value="Uganda"> Uganda </asp:ListItem>
        <asp:ListItem value="Ukraine"> Ukraine </asp:ListItem>
        <asp:ListItem value="United Arab Emirates"> United Arab Emirates </asp:ListItem>
        <asp:ListItem value="United Kingdom"> United Kingdom </asp:ListItem>
        <asp:ListItem value="United States"> United States </asp:ListItem>
        <asp:ListItem value="Uruguay"> Uruguay </asp:ListItem>
        <asp:ListItem value="Uzbekistan"> Uzbekistan </asp:ListItem>
        <asp:ListItem value="Vanuatu"> Vanuatu </asp:ListItem>
        <asp:ListItem value="Vatican City"> Vatican City </asp:ListItem>
        <asp:ListItem value="Venezuela"> Venezuela </asp:ListItem>
        <asp:ListItem value="Vietnam"> Vietnam </asp:ListItem>
        <asp:ListItem value="British Virgin Islands"> British Virgin Islands </asp:ListItem>
        <asp:ListItem value="Isle of Man"> Isle of Man </asp:ListItem>
        <asp:ListItem value="US Virgin Islands"> US Virgin Islands </asp:ListItem>
        <asp:ListItem value="Wallis and Futuna"> Wallis and Futuna </asp:ListItem>
        <asp:ListItem value="Western Sahara"> Western Sahara </asp:ListItem>
        <asp:ListItem value="Yemen"> Yemen </asp:ListItem>
        <asp:ListItem value="Zambia"> Zambia </asp:ListItem>
        <asp:ListItem value="Zimbabwe"> Zimbabwe </asp:ListItem>
	</asp:DropDownList>
	<p></p>
	<p>
		<asp:TextBox ID="phoneNumberBox" runat="server" placeholder="Phone Number"></asp:TextBox>
		<asp:Label ID="phoneNumReturn" runat="server" Text=""></asp:Label>
	</p>
	<p>
		<asp:Button ID="registerButton" runat="server" Text="Register" OnClick="registerButton_Click" />
		<asp:Label ID="returnLabel" runat="server" Text="" CssClass="returnLabel"></asp:Label>
	</p>
</asp:Content>
