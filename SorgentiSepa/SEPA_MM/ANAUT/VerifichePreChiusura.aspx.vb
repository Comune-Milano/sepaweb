
Partial Class ANAUT_VerifichePreChiusura
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
           & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
           & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
           & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
           & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
           & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
           & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
           & "</td></tr></table></div></div>"
        Response.Write(Loading)

        If Not IsPostBack Then
            Response.Flush()
            CaricaDatiAU()
            CaricaStrutture()
            'CaricaTipoContratti()
            CaricaTipoContrattiNew()
            CaricaSindacati()
        End If

        txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        txtSloggioDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtSloggioAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Function CaricaTipoContrattiNew()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                PAR.SettaCommand(PAR)
            End If

            PAR.cmd.CommandText = "SELECT * FROM UTENZA_BANDI where id=" & cmbAU.SelectedItem.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                If PAR.IfNull(myReader("erp_1"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP SOCIALE", "1"))
                End If

                If PAR.IfNull(myReader("erp_2"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP MODERATO", "2"))
                End If

                If PAR.IfNull(myReader("erp_FF_OO"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP FF.OO.", "3"))
                End If

                If PAR.IfNull(myReader("erp_art_22"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP ART.22 C.10", "4"))
                End If

                If PAR.IfNull(myReader("erp_conv"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP CONVENZIONATO", "5"))
                End If

                If PAR.IfNull(myReader("ERP_4"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP art.15 comma 2-vizi amministrativi", "6"))
                End If

                If PAR.IfNull(myReader("ERP_3"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP Contratti precari Art. 15 let. a, b", "7"))
                End If

                If PAR.IfNull(myReader("L39278"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("392/78", "8"))
                End If

                If PAR.IfNull(myReader("L43198"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("431/98", "9"))
                End If

                If PAR.IfNull(myReader("OA"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("Occupazioni Abusive", "10"))
                End If

            End If
            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Dim i As Integer = 0
            For i = 0 To chListRU.Items.Count - 1
                chListru.Items(i).Selected = True
            Next

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function


    Private Function CaricaSindacati()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbSindacato.Items.Add(New ListItem("TUTTI", "0"))
            PAR.cmd.CommandText = "SELECT * FROM sindacati_vsa"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read
                cmbSindacato.Items.Add(New ListItem(PAR.IfNull(myReader("descrizione"), ""), myReader("id")))
            Loop

            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    'Private Function CaricaTipoContratti()
    '    Try
    '        If PAR.OracleConn.State = Data.ConnectionState.Closed Then
    '            PAR.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If
    '        cmbTipoContratto.Items.Clear()
    '        cmbTipoContratto.Items.Add(New ListItem("TUTTI", "0"))
    '        PAR.cmd.CommandText = "SELECT * FROM UTENZA_BANDI where id=" & cmbAU.SelectedItem.Value
    '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
    '        If myReader.Read Then
    '            If PAR.IfNull(myReader("erp_1"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP SOCIALE", "1"))
    '            End If

    '            If PAR.IfNull(myReader("erp_2"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP MODERATO", "2"))
    '            End If

    '            If PAR.IfNull(myReader("erp_FF_OO"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP FF.OO.", "3"))
    '            End If

    '            If PAR.IfNull(myReader("erp_art_22"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP ART.22 C.10", "4"))
    '            End If

    '            If PAR.IfNull(myReader("erp_conv"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP CONVENZIONATO", "5"))
    '            End If

    '            If PAR.IfNull(myReader("ERP_4"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP art.15 comma 2-vizi amministrativi", "6"))
    '            End If

    '            If PAR.IfNull(myReader("ERP_3"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP Contratti precari Art. 15 let. a, b", "7"))
    '            End If

    '            If PAR.IfNull(myReader("L39278"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("392/78", "8"))
    '            End If

    '            If PAR.IfNull(myReader("L43198"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("431/98", "9"))
    '            End If

    '            If PAR.IfNull(myReader("OA"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("Occupazioni Abusive", "10"))
    '            End If

    '        End If
    '        myReader.Close()

    '        PAR.OracleConn.Close()
    '        PAR.cmd.Dispose()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '    Catch ex As Exception
    '        PAR.OracleConn.Close()
    '        PAR.cmd.Dispose()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '    End Try
    'End Function

    Private Function CaricaDatiAU()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            PAR.cmd.CommandText = "SELECT * FROM UTENZA_BANDI order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read
                cmbAU.Items.Add(New ListItem(PAR.IfNull(myReader("descrizione"), ""), PAR.IfNull(myReader("id"), "0")))
            Loop
            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Function CaricaStrutture()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            chListStrutture.Items.Clear()
            PAR.cmd.CommandText = "SELECT UTENZA_FILIALI.*,TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI,UTENZA_FILIALI WHERE utenza_filiali.fl_eliminato=0 and TAB_FILIALI.ID=UTENZA_filiali.ID_STRUTTURA AND ID_BANDO=" & cmbAU.SelectedItem.Value & " AND TUTTO_PATRIMONIO=0 order by nome asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read
                chListStrutture.Items.Add(New ListItem(PAR.IfNull(myReader("NOME"), ""), PAR.IfNull(myReader("id"), "0")))
            Loop
            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Function CaricaSportelli()
        Try
            Dim i As Integer = 0
            Dim s As String = ""

            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            chListSportelli.Items.Clear()

            For i = 0 To chListStrutture.Items.Count - 1
                If chListStrutture.Items(i).Selected = True Then
                    s = s & chListStrutture.Items(i).Value & ","
                End If
            Next
            If s <> "" Then
                s = Mid(s, 1, Len(s) - 1)
                s = " WHERE utenza_sportelli.fl_eliminato=0 and ID_FILIALE IN (" & s & ")"

                PAR.cmd.CommandText = "SELECT UTENZA_SPORTELLI.* FROM UTENZA_SPORTELLI " & s & " order by DESCRIZIONE asc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                Do While myReader.Read
                    chListSportelli.Items.Add(New ListItem(PAR.IfNull(myReader("DESCRIZIONE"), ""), PAR.IfNull(myReader("id"), "0")))
                Loop
                myReader.Close()

            End If



            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Protected Sub cmbAU_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAU.SelectedIndexChanged
        CaricaStrutture()
        CaricaTipoContrattiNew()
    End Sub

    Protected Sub chListStrutture_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles chListStrutture.SelectedIndexChanged
        CaricaSportelli()
    End Sub

    Private Sub MessJQuery(ByVal Messaggio As String, ByVal Tipo As Integer, Optional ByVal Titolo As String = "Messaggio")
        Try
            Dim sc As String = ""
            If Tipo = 0 Then
                sc = ScriptErrori(Messaggio, Titolo)
            Else
                sc = ScriptChiudi(Messaggio, Titolo)
            End If
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptMsg", sc, True)
        Catch ex As Exception
            'lblErrore.Text = ex.Message
            'lblErrore.Visible = True
        End Try
    End Sub
    Private Function ScriptErrori(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function ScriptChiudi(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');self.close();}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub SceltaJQuery(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "")
        Try
            Dim sc As String = ScriptScelta(Messaggio, Funzione, Titolo, Funzione2)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptScelta", sc, True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function ScriptScelta(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptScelta').text('" & Messaggio & "');")
            sb.Append("$('#ScriptScelta').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "', buttons: {'Si': function() { __doPostBack('" & Funzione & "', '');{$(this).dialog('close');} },'No': function() {$(this).dialog('close');" & Funzione2 & "}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            Dim trovato As Boolean = False
            Dim b As Boolean = False
            Dim stringaSQL As String = ""
            Dim stringaSQL1 As String = ""

            For i = 0 To chListStrutture.Items.Count - 1
                If chListStrutture.Items(i).Selected = True Then
                    trovato = True
                End If
            Next
            If trovato = False Then
                Response.Write("<script>alert('Selezionare almeno una struttura!');</script>")
                Exit Sub
            End If

            trovato = False
            For i = 0 To chListSportelli.Items.Count - 1
                If chListSportelli.Items(i).Selected = True Then
                    trovato = True
                End If
            Next
            If trovato = False Then
                Response.Write("<script>alert('Selezionare almeno uno sportello/sede!');</script>")
                Exit Sub
            End If

            If cmbCompDa.SelectedItem.Text <> "--" And cmbCompA.SelectedItem.Text <> "" Then
                If cmbCompDa.SelectedItem.Text > CInt(Replace(cmbCompA.SelectedItem.Text, "--", "100")) Then
                    Response.Write("<script>alert('Intervallo numero componenti errato!');</script>")
                    Exit Sub
                End If
            End If


            stringaSQL = " CONVOCAZIONI_AU.ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & cmbAU.SelectedItem.Value & ") "
            stringaSQL1 = " CONVOCAZIONI_AU.ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & cmbAU.SelectedItem.Value & ") "
            b = True

            Dim s As String = ""
            Dim strutture As String = ""

            For i = 0 To chListStrutture.Items.Count - 1
                If chListStrutture.Items(i).Selected = True Then
                    s = s & chListStrutture.Items(i).Value & ","
                    strutture = strutture & chListStrutture.Items(i).Text & ","
                End If
            Next
            If s <> "" Then
                s = Mid(s, 1, Len(s) - 1)
                If b = True Then
                    s = " AND CONVOCAZIONI_AU.ID_FILIALE IN (SELECT ID_STRUTTURA FROM UTENZA_FILIALI WHERE ID IN (" & s & "))"
                Else
                    s = " CONVOCAZIONI_AU.ID_FILIALE IN (SELECT ID_STRUTTURA FROM UTENZA_FILIALI WHERE ID IN (" & s & "))"
                End If
                b = True
            End If
            stringaSQL = stringaSQL & s
            stringaSQL1 = stringaSQL1 & s

            s = ""
            Dim Sportelli As String = ""
            For i = 0 To chListSportelli.Items.Count - 1
                If chListSportelli.Items(i).Selected = True Then
                    s = s & chListSportelli.Items(i).Value & ","
                    Sportelli = Sportelli & chListSportelli.Items(i).Text & ","
                End If
            Next
            If s <> "" Then
                s = Mid(s, 1, Len(s) - 1)
                If b = True Then
                    s = " AND CONVOCAZIONI_AU.ID_SPORTELLO IN (" & s & ")"
                Else
                    s = " CONVOCAZIONI_AU.ID_SPORTELLO IN (" & s & ")"
                End If
                b = True
            End If

            stringaSQL = stringaSQL & s
            stringaSQL1 = stringaSQL1 & s

            Dim NumComponenti As String = "Tutti"

            s = ""
            If cmbCompDa.SelectedItem.Text <> "--" Or cmbCompA.SelectedItem.Text <> "--" Then
                If cmbCompDa.SelectedItem.Text <> "--" And cmbCompA.SelectedItem.Text = "--" Then
                    If b = True Then
                        s = " AND UTENZA_DICHIARAZIONI.N_COMP_NUCLEO>=" & cmbCompDa.SelectedItem.Text
                    Else
                        s = " UTENZA_DICHIARAZIONI.N_COMP_NUCLEO>=" & cmbCompDa.SelectedItem.Text
                    End If
                    NumComponenti = "da " & cmbCompDa.SelectedItem.Text
                End If
                If cmbCompDa.SelectedItem.Text = "--" And cmbCompA.SelectedItem.Text <> "--" Then
                    If b = True Then
                        s = " AND UTENZA_DICHIARAZIONI.N_COMP_NUCLEO<=" & cmbCompA.SelectedItem.Text
                    Else
                        s = " UTENZA_DICHIARAZIONI.N_COMP_NUCLEO<=" & cmbCompA.SelectedItem.Text
                    End If
                    NumComponenti = "fino a " & cmbCompA.SelectedItem.Text
                End If
                If cmbCompDa.SelectedItem.Text <> "--" And cmbCompA.SelectedItem.Text <> "--" Then
                    If b = True Then
                        s = " AND UTENZA_DICHIARAZIONI.N_COMP_NUCLEO>=" & cmbCompDa.SelectedItem.Text & " AND UTENZA_DICHIARAZIONI.N_COMP_NUCLEO<=" & cmbCompA.SelectedItem.Text
                    Else
                        s = " UTENZA_DICHIARAZIONI.N_COMP_NUCLEO>=" & cmbCompDa.SelectedItem.Text & " AND UTENZA_DICHIARAZIONI.N_COMP_NUCLEO<=" & cmbCompA.SelectedItem.Text
                    End If
                    NumComponenti = "Da " & cmbCompDa.SelectedItem.Text & " fino a " & cmbCompA.SelectedItem.Text
                End If

                b = True
            End If
            stringaSQL = stringaSQL & s


            s = ""
            Dim NumComponenti65 As String = "Indifferente"

            If cmb65.SelectedItem.Text = "SI" Or cmb65.SelectedItem.Text = "NO" Then
                If cmb65.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND NVL(UTENZA_DICHIARAZIONI.PRESENZA_MAG_65,0)>0"
                    Else
                        s = " NVL(UTENZA_DICHIARAZIONI.PRESENZA_MAG_65,0)>0"
                    End If
                    NumComponenti65 = "SI"
                End If

                If cmb65.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND NVL(UTENZA_DICHIARAZIONI.PRESENZA_MAG_65,0)=0"
                    Else
                        s = " NVL(UTENZA_DICHIARAZIONI.PRESENZA_MAG_65,0)=0"
                    End If
                    NumComponenti65 = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s


            s = ""
            Dim NumComponenti15 As String = "Indifferente"

            If cmb15.SelectedItem.Text = "SI" Or cmb15.SelectedItem.Text = "NO" Then
                If cmb15.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND NVL(UTENZA_DICHIARAZIONI.PRESENZA_MIN_15,0)>0"
                    Else
                        s = " NVL(UTENZA_DICHIARAZIONI.PRESENZA_MIN_15,0)>0"
                    End If
                    NumComponenti15 = "SI"
                End If

                If cmb15.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND NVL(UTENZA_DICHIARAZIONI.PRESENZA_MIN_15,0)=0"
                    Else
                        s = " NVL(UTENZA_DICHIARAZIONI.PRESENZA_MIN_15,0)=0"
                    End If
                    NumComponenti15 = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s

            s = ""
            Dim NumComponenti6699 As String = "Indifferente"

            If cmb6699.SelectedItem.Text = "SI" Or cmb6699.SelectedItem.Text = "NO" Then
                If cmb6699.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_66,0)>0"
                    Else
                        s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_66,0)>0"
                    End If
                    NumComponenti6699 = "SI"
                End If

                If cmb6699.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_66,0)=0"
                    Else
                        s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_66,0)=0"
                    End If
                    NumComponenti6699 = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s

            s = ""
            Dim NumComponenti100Non As String = "Indifferente"

            If cmb100Non.SelectedItem.Text = "SI" Or cmb100Non.SelectedItem.Text = "NO" Then
                If cmb100Non.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_SENZA,0)<>'0'"
                    Else
                        s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_SENZA,0)<>'0'"
                    End If
                    NumComponenti100Non = "SI"
                End If

                If cmb100Non.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_SENZA,0)='0'"
                    Else
                        s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_SENZA,0)='0'"
                    End If
                    NumComponenti100Non = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s

            s = ""
            Dim NumComponenti100Acc As String = "Indifferente"

            If cmb100Acc.SelectedItem.Text = "SI" Or cmb100Acc.SelectedItem.Text = "NO" Then
                If cmb100Acc.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_CON,0)<>'0'"
                    Else
                        s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_CON,0)<>'0'"
                    End If
                    NumComponenti100Acc = "SI"
                End If

                If cmb100Acc.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_CON,0)='0'"
                    Else
                        s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_CON,0)='0'"
                    End If
                    NumComponenti100Acc = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s


            s = ""
            Dim RedditoDip As String = "Indifferente"

            If cmbDip.SelectedItem.Text = "SI" Or cmbDip.SelectedItem.Text = "NO" Then
                If cmbDip.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND (SELECT NVL(SUM(IMPORTO),0) FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID))<>0 "
                    Else
                        s = " (SELECT NVL(SUM(IMPORTO),0) FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID))<>0 "
                    End If
                    RedditoDip = "SI"
                End If

                If cmbDip.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND (SELECT NVL(SUM(IMPORTO),0) FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID))=0 "
                    Else
                        s = " (SELECT NVL(SUM(IMPORTO),0) FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID))=0 "
                    End If
                    RedditoDip = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s

            s = ""
            Dim RedditoImm As String = "Indifferente"

            If cmbImmobiliare.SelectedItem.Text = "SI" Or cmbImmobiliare.SelectedItem.Text = "NO" Then
                If cmbImmobiliare.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND (SELECT NVL(SUM(VALORE),0) FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID)) <>0"
                    Else
                        s = " (SELECT NVL(SUM(VALORE),0) FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID)) <>0"
                    End If
                    RedditoImm = "SI"
                End If

                If cmbImmobiliare.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND (SELECT NVL(SUM(VALORE),0) FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID)) =0"
                    Else
                        s = " (SELECT NVL(SUM(VALORE),0) FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID)) =0"
                    End If
                    RedditoImm = "NO"
                End If
                b = True
            End If
            stringaSQL = stringaSQL & s

            s = ""
            Dim TUTORESTR As String = "Indifferente"
            If cmbTutore.SelectedItem.Text = "SI" Or cmbTutore.SelectedItem.Text = "NO" Then
                If cmbTutore.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =1 "
                        TUTORESTR = "SI"
                    Else
                        s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =1 "
                        TUTORESTR = "NO"
                    End If
                End If
                If cmbTutore.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =0 "
                        TUTORESTR = "SI"
                    Else
                        s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =0 "
                        TUTORESTR = "NO"
                    End If
                End If
            End If
            stringaSQL = stringaSQL & s
            stringaSQL1 = stringaSQL1 & s

            s = ""
            Dim SINDACATO As String = "Indifferente"
            If cmbSindacato.SelectedItem.Text <> "TUTTI" Then
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.SINDACATO = " & cmbSindacato.SelectedItem.Value
                    SINDACATO = cmbSindacato.SelectedItem.Text
                Else
                    s = " RAPPORTI_UTENZA.SINDACATO =" & cmbSindacato.SelectedItem.Value
                    SINDACATO = cmbSindacato.SelectedItem.Text
                End If

            End If
            stringaSQL = stringaSQL & s
            stringaSQL1 = stringaSQL1 & s

            s = ""
            Dim stipula As String = ""
            If txtStipulaDal.Text <> "" Then
                stipula = "Dal " & txtStipulaDal.Text
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.data_stipula> = '" & PAR.AggiustaData(txtStipulaDal.Text) & "' "
                Else
                    s = " RAPPORTI_UTENZA.data_stipula> = '" & PAR.AggiustaData(txtStipulaDal.Text) & "' "
                End If
            End If
            stringaSQL = stringaSQL & s
            stringaSQL1 = stringaSQL1 & s

            s = ""
            If txtStipulaAl.Text <> "" Then
                stipula = stipula & " fino al " & txtStipulaAl.Text
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.data_stipula< = '" & PAR.AggiustaData(txtStipulaAl.Text) & "' "
                Else
                    s = " RAPPORTI_UTENZA.data_stipula< = '" & PAR.AggiustaData(txtStipulaAl.Text) & "' "
                End If
            End If
            stringaSQL = stringaSQL & s
            stringaSQL1 = stringaSQL1 & s

            If stipula = "" Then stipula = "Indifferente"

            s = ""
            Dim sloggio As String = ""
            If txtSloggioDal.Text <> "" Then
                sloggio = "Dal " & txtSloggioDal.Text
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.data_riconsegna> = '" & PAR.AggiustaData(txtSloggioDal.Text) & "' "
                Else
                    s = " RAPPORTI_UTENZA.data_riconsegna> = '" & PAR.AggiustaData(txtSloggioDal.Text) & "' "
                End If
            End If
            stringaSQL = stringaSQL & s
            stringaSQL1 = stringaSQL1 & s

            s = ""
            If txtSloggioAl.Text <> "" Then
                sloggio = sloggio & " fino al " & txtSloggioAl.Text
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.data_riconsegna< = '" & PAR.AggiustaData(txtSloggioAl.Text) & "' "
                Else
                    s = " RAPPORTI_UTENZA.data_riconsegna< = '" & PAR.AggiustaData(txtSloggioAl.Text) & "' "
                End If
            End If
            stringaSQL = stringaSQL & s
            stringaSQL1 = stringaSQL1 & s

            If sloggio = "" Then sloggio = "Indifferente"

            's = ""
            'Dim tipoC As String = ""
            'Dim tipologiaC As String = "Indifferente"
            'If cmbTipoContratto.SelectedItem.Text <> "TUTTI" Then
            '    tipologiaC = cmbTipoContratto.SelectedItem.Text
            '    tipoC = cmbTipoContratto.SelectedItem.Value
            'End If

            s = "("
            Dim tipoC As String = ""
            Dim tipologiaC As String = ""
            For i = 0 To chListRU.Items.Count - 1
                If chListRU.Items(i).Selected = True Then
                    's = s & chListRU.Items(i).Value & ","
                    tipologiaC = tipologiaC & chListRU.Items(i).Text & ","
                    Select Case chListRU.Items(i).Value
                        Case "1"
                            s = s & " (rapporti_utenza.provenienza_ass = 1 AND unita_immobiliari.id_destinazione_uso <> 2) or "
                        Case "2"
                            s = s & " unita_immobiliari.id_destinazione_uso = 2 or "
                        Case "3"
                            's = s & " unita_immobiliari.id_destinazione_uso = 10 or "
                            s = s & " rapporti_utenza.provenienza_ass = 10 or "
                        Case "4"
                            s = s & " rapporti_utenza.provenienza_ass = 8 or "
                        Case "5"
                            s = s & " unita_immobiliari.id_destinazione_uso = 12 or "
                        Case "6"

                        Case "7"
                            s = s & " rapporti_utenza.dest_uso = 'D' OR "
                        Case "8"

                        Case "9"

                        Case "10"
                            s = s & " rapporti_utenza.provenienza_ass = 7 or "
                    End Select
                End If
            Next
            If b = True Then s = " and " & s
            If s = s & " (" Then
                s = " (rapporti_utenza.dest_uso='XXX') "
            Else
                s = Mid(s, 1, Len(s) - 4) & ")  "
            End If

            stringaSQL = stringaSQL & s & " and "
            stringaSQL1 = stringaSQL1 & s & " and "
            Session.Add("SX", stringaSQL1)
            Session.Add("SX1", stringaSQL)
            'Response.Write("<script>window.open('ElencoConvocabili.aspx?Q=" & PAR.Cripta("STRUTTURE:" & strutture & "-SPORTELLI:" & Sportelli & "-Num.Comp.:" & NumComponenti & "-Maggiori 65:" & NumComponenti65 & "-minori 15:" & NumComponenti15 & "-Comp.Inv. 66-99%:" & NumComponenti6699 & "-Comp.Inv.100% No ACC.:" & NumComponenti100Non & "-Comp.Inv.100% Acc.:" & NumComponenti100Acc & "-Redd.Prev.Dip.:" & RedditoDip & "-Redd.Immob.:" & RedditoImm & "-Tutore Str.:" & TUTORESTR & "-Sindacato Rif.:" & SINDACATO & "-Stipula:" & stipula & "-Sloggio:" & sloggio & "-Tipo Rapporto:" & tipologiaC) & "&TIPOC=" & tipoC & "&IDB=" & cmbAU.SelectedItem.Value & "&S=" & PAR.Cripta(stringaSQL) & "','','');</script>")

            Response.Write("<script>window.open('ElencoVerificheChiusura.aspx?Q=" & PAR.Cripta("STRUTTURE:" & strutture & "-SPORTELLI:" & Sportelli & "-Num.Comp.:" & NumComponenti & "-Maggiori 65:" & NumComponenti65 & "-minori 15:" & NumComponenti15 & "-Comp.Inv. 66-99%:" & NumComponenti6699 & "-Comp.Inv.100% No ACC.:" & NumComponenti100Non & "-Comp.Inv.100% Acc.:" & NumComponenti100Acc & "-Redd.Prev.Dip.:" & RedditoDip & "-Redd.Immob.:" & RedditoImm & "-Tutore Str.:" & TUTORESTR & "-Sindacato Rif.:" & SINDACATO & "-Stipula:" & stipula & "-Sloggio:" & sloggio & "-Tipo Rapporto:" & tipologiaC) & "&TIPOC=" & tipoC & "&IDB=" & cmbAU.SelectedItem.Value & "&S=" & PAR.Cripta(stringaSQL) & "','','');</script>")

        Catch ex As Exception

        End Try
    End Sub
End Class
