
Partial Class CAMBI_Correlazioni2
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreCF As String
    Dim sValoreID As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            sValoreCF = par.PulisciStrSql(Request.QueryString("CF"))
            sValoreID = par.PulisciStrSql(Request.QueryString("ID"))
            strConness = Request.QueryString("CONN")
            If Request.QueryString("NUOVA") = "1" Then
                Button1.Visible = True
            Else
                Button1.Visible = False
            End If
            Label4.Text = "Correlazioni trovate Cod. Fiscale " & sValoreCF
            If Len(sValoreCF) = 16 Then
                If IsNumeric(sValoreID) Then
                    s_Stringasql = "SELECT UTENZA_DICHIARAZIONI.ID,UTENZA_DICHIARAZIONI.PG ,TO_CHAR(TO_DATE(UTENZA_DICHIARAZIONI.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS  ""DATA_PG"",DECODE(UTENZA_COMP_NUCLEO.PROGR,0,'RICHIEDENTE') AS ""INTESTAZIONE"" FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_DICHIARAZIONI.ID=UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE (+) AND (UTENZA_COMP_NUCLEO.COD_FISCALE='" & sValoreCF & "' OR UTENZA_COMP_NUCLEO.COD_FISCALE='" & sValoreCF & "') AND UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE<>" & sValoreID & " ORDER BY UTENZA_COMP_NUCLEO.COD_FISCALE ASC"
                    Verifica()
                End If
            End If
            Button1.Enabled = False


        End If
        '**********modifiche campi***********
        Dim CTRL As Control
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is Button Then
                DirectCast(CTRL, Button).Attributes.Add("onclick", "javascript:opener.parent.main.document.getElementById('txtModificato').value='1';opener.parent.main.document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            End If
        Next

    End Sub

    Private Function Verifica()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(s_Stringasql, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        par.OracleConn.Close()
        par.OracleConn.Dispose()

    End Function

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Public Property s_Stringasql() As String
        Get
            If Not (ViewState("par_s_Stringasql") Is Nothing) Then
                Return CStr(ViewState("par_s_Stringasql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_s_Stringasql") = value
        End Set

    End Property

    Public Property s_DIC() As String
        Get
            If Not (ViewState("par_s_DIC") Is Nothing) Then
                Return CStr(ViewState("par_s_DIC"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_s_DIC") = value
        End Set

    End Property

    Public Property s_Stringasql1() As String
        Get
            If Not (ViewState("par_s_Stringasql1") Is Nothing) Then
                Return CStr(ViewState("par_s_Stringasql1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_s_Stringasql1") = value
        End Set

    End Property



    Public Property s_Valore() As String
        Get
            If Not (ViewState("par_s_Valore") Is Nothing) Then
                Return CStr(ViewState("par_s_Valore"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_s_Valore") = value
        End Set

    End Property

    Public Property strConness() As String
        Get
            If Not (ViewState("par_strConness") Is Nothing) Then
                Return CStr(ViewState("par_strConness"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_strConness") = value
        End Set
    End Property

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        s_Stringasql1 = "SELECT UTENZA_COMP_NUCLEO.COGNOME,UTENZA_COMP_NUCLEO.NOME,UTENZA_COMP_NUCLEO.COD_FISCALE ,TO_CHAR(TO_DATE(UTENZA_COMP_NUCLEO.DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS  ""DATA_NASCITA"",T_TIPO_PARENTELA.DESCRIZIONE AS ""PARENTELA"" FROM T_TIPO_PARENTELA,UTENZA_COMP_NUCLEO WHERE UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE=" & e.Item.Cells(0).Text & " AND UTENZA_COMP_NUCLEO.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY UTENZA_COMP_NUCLEO.PROGR ASC"
        s_DIC = e.Item.Cells(0).Text
        s_Valore = e.Item.Cells(1).Text
        'H1.Value = "" & e.Item.Cells(0).Text & ""
        'Button1.Attributes.Add("OnClick", "javascript:opener.parent.main.ImportNucleo(" & e.Item.Cells(0).Text & ");")
        AggiornaComp()
        Button1.Enabled = True
    End Sub

    Function AggiornaComp()
        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(s_Stringasql1, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO")
        DataGrid2.DataSource = ds
        DataGrid2.DataBind()
        Label2.Text = "Composizione Nucleo PG " & s_Valore
        par.OracleConn.Close()
        par.OracleConn.Dispose()
    End Function

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            Verifica()
        End If
    End Sub

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampa.Click
        Response.Write("<script>window.print();</script>")
    End Sub

    Protected Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid2.CurrentPageIndex = e.NewPageIndex
            AggiornaComp()
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        par.OracleConn = CType(HttpContext.Current.Session.Item(strConness), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & strConness), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        par.cmd.CommandText = s_Stringasql1
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            If myReader("COD_FISCALE") = par.PulisciStrSql(Request.QueryString("CF")) Then
                par.cmd.CommandText = "DELETE FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & par.PulisciStrSql(Request.QueryString("ID")) & " AND COD_FISCALE<>'" & par.PulisciStrSql(Request.QueryString("CF")) & "'"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & s_DIC & " AND COD_FISCALE<>'" & par.PulisciStrSql(Request.QueryString("CF")) & "' ORDER BY PROGR ASC"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL,DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_COMP_NUCLEO_VSA.NEXTVAL," & par.PulisciStrSql(Request.QueryString("ID")) & "," & myReader1("PROGR") & ",'" _
                                & par.PulisciStrSql(myReader1("COGNOME")) & "','" _
                                & par.PulisciStrSql(myReader1("NOME")) & "'," _
                                & myReader1("GRADO_PARENTELA") & ",'" _
                                & par.PulisciStrSql(myReader1("COD_FISCALE")) & "','" _
                                & par.PulisciStrSql(myReader1("PERC_INVAL")) & "','" _
                                & par.PulisciStrSql(myReader1("DATA_NASCITA")) & "','" _
                                & par.PulisciStrSql(par.IfNull(myReader1("USL"), "")) & "','" _
                                & myReader1("INDENNITA_ACC") & "','" & myReader1("SESSO") & "')"
                    par.cmd.ExecuteNonQuery()
                End While
                myReader1.Close()
                myReader.Close()

                Response.Write("<script>opener.form1.HiddenField1.value='1';opener.form1.submit();window.close();</script>")
            Else
                Dim ScriptBlock As String

                ScriptBlock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Non è possibile effettuare questa operazione perchè il dichiarante del nucleo correlato deve coincidere con il titolare della dichiarazione che si sta inserendo!');" _
                    & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript142344")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript142344", ScriptBlock)
                End If
            End If
        End If
        myReader.Close()

        par.myTrans.Commit()
        par.myTrans = par.OracleConn.BeginTransaction()
        HttpContext.Current.Session.Add("TRANSAZIONE" & strConness, par.myTrans)

    End Sub


End Class
