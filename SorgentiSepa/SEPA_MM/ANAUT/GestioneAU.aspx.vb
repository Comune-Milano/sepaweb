Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class ANAUT_GestioneAU
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try


            ' Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            '& "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            '& "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            '& "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
            '& "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            '& "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            '& "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            '& "</td></tr></table></div></div>"
            ' Response.Write(Loading)
            ' Response.Flush()
            If Not IsPostBack Then

                CercaAperte()
            End If
            If Session.Item("MOD_AU_ELIMINA_BANDO") = "1" Then
                btnElimina.Visible = True
            Else
                btnElimina.Visible = False
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
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
            sb.Append("$('#ScriptScelta').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "', buttons: {'Si': function() { __doPostBack('" & Funzione & "', '');{$(this).dialog('close');document.getElementById('divLoading').style.visibility = 'visible';} },'No': function() {$(this).dialog('close');" & Funzione2 & "}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Private Sub Cerca()
        sStringaSQL1 = "select UTENZA_BANDI.ANNO_AU,UTENZA_bandi.ID,UTENZA_BANDI.DESCRIZIONE,to_char(to_date(UTENZA_BANDI.DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') as DATA_INIZIO,to_char(to_date(UTENZA_BANDI.DATA_FINE,'yyyymmdd'),'dd/mm/yyyy') as DATA_FINE,UTENZA_BANDI.ANNO_ISEE,TAB_STATI_BANDO.descrizione as descrizione_stato from UTENZA_bandi,TAB_STATI_BANDO where UTENZA_bandi.id<>-1 and TAB_STATI_BANDO.id=UTENZA_bandi.stato order by id desc"
        BindGrid()
    End Sub

    Private Sub CercaAperte()
        sStringaSQL1 = "select UTENZA_BANDI.ANNO_AU,UTENZA_bandi.ID,UTENZA_BANDI.DESCRIZIONE,to_char(to_date(UTENZA_BANDI.DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') as DATA_INIZIO,to_char(to_date(UTENZA_BANDI.DATA_FINE,'yyyymmdd'),'dd/mm/yyyy') as DATA_FINE,UTENZA_BANDI.ANNO_ISEE,TAB_STATI_BANDO.descrizione as descrizione_stato from UTENZA_bandi,TAB_STATI_BANDO where UTENZA_bandi.id<>-1 and TAB_STATI_BANDO.id=UTENZA_bandi.stato and (utenza_bandi.stato=1 or utenza_bandi.stato=0) order by id desc"
        BindGrid()
    End Sub

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Private Sub BindGrid()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "UTENZA_BANDI")
            Label4.Text = Datagrid2.Items.Count
            Datagrid2.DataSource = ds
            Datagrid2.DataBind()
            Label4.Text = " - " & Datagrid2.Items.Count & " nella lista"

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub
    Protected Sub Datagrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")

            'btnVisualizza.Attributes.Add("onclick", "window.open('Contratto.aspx?ID=" & LBLID.Text & "&COD=" & Label3.Text & "','Contratto" & Format(Now, "hhss") & "','height=680,width=900');")
        End If
    End Sub

    Protected Sub Datagrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            Datagrid2.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub


    Private Function VerificaLettere()
        par.cmd.CommandText = "SELECT * FROM UTENZA_LISTE WHERE LETTERA_CREATA=1 AND ID IN (SELECT ID_LISTA_CONV FROM UTENZA_LISTE_CDETT WHERE ID_LISTA IN (SELECT ID FROM UTENZA_LISTE_CONV WHERE ID_AU=" & LBLID.Value & "))"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.HasRows = True Then
            SceltaJQuery("Sono state già stampate delle lettere di convocazione per l\'AU selezionata. Si desidera procedere ugualmente? Tutte le lettere dovranno essere rifatte e il sistema procederà all\'eliminazione di tutti i dati relativi all\'AU (Chiusure programmate, filiali, sportelli, operatori, etc).", "btnEliminaBando", "Attenzione...", "")
        Else
            SceltaJQuery("L\'Anagrafe Utenza selezioanta è in stato APERTA! Il sistema procederà all\'eliminazione e tutti i dati ad essa legati (Chiusure programmate, filiali, sportelli, operatori, etc) saranno eliminati in maniera definitiva.", "btnEliminaBando", "Attenzione...", "")
        End If
        myReader1.Close()
    End Function

    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        If H1.Value = "1" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE id=" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then

                    Select Case myReader("stato")
                        Case "0"
                            SceltaJQuery("Eliminare l\'Anagrafe Utenza selezionata adesso? Non sarà più possibile modificare o eliminare.", "btnEliminaBando", "Attenzione...", "")
                        Case "1"
                            VerificaLettere()
                        Case "2"
                            MessJQuery("L\'Anagrafe Utenza selezioanta è in stato CHIUSA e non può essere eliminata!", 0, "Attenzione")
                    End Select

                End If
                myReader.Close()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Try
            Response.Redirect("pagina_home.aspx", False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuovo.Click
        BindGrid()
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        BindGrid()
    End Sub

    Protected Sub btnApreChiude_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApreChiude.Click
        If H1.Value = "1" Then
            VerificaApriChiudi()
            'MessJQuery("Valorizzare tutti i campi prima di salvare!", 0, "Attenzione")
        End If
    End Sub

    Private Function VerificaApriChiudi()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim DA_COMPLETARE As Integer = 0
            Dim DA_CANCELLARE As Integer = 0
            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE id=" & LBLID.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then

                Select Case myReader("stato")
                    Case "0"
                        SceltaJQuery("Aprire l\'Anagrafe Utenza selezionata adesso? Non sarà più possibile modificare o eliminare", "btnApri", "Attenzione...", "")
                    Case "1"

                        par.cmd.CommandText = "SELECT COUNT(ID) FROM UTENZA_DICHIARAZIONI WHERE ID_BANDO=" & myReader("ID") & " and id_stato=0"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            DA_COMPLETARE = par.IfNull(myReader1(0), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT COUNT(ID) FROM UTENZA_DICHIARAZIONI WHERE ID_BANDO=" & myReader("ID") & " and id_stato=2"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            DA_CANCELLARE = par.IfNull(myReader1(0), "0")
                        End If
                        myReader1.Close()

                        SceltaJQuery("Risultano ancora " & DA_COMPLETARE & " Dichiarazioni DA COMPLETARE e " & DA_CANCELLARE & " Dichiarazioni DA CANCELLARE. Procedere ugualmente con la chiusura dell\'Anagrafe Utenza?", "btnChiudi", "Attenzione...", "")

                    Case "2"
                        MessJQuery("Il bando selezionato è CHIUSO!", 0, "Attenzione")
                End Select

            End If
            myReader.Close()
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function

    Protected Sub btnApri_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApri.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "UPDATE UTENZA_BANDI SET STATO=1 WHERE ID=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT * FROM UTENZA_FILIALI_DEFAULT"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader1.Read
                par.cmd.CommandText = "INSERT INTO UTENZA_FILIALI (ID,ID_STRUTTURA,ID_BANDO,TUTTO_PATRIMONIO) VALUES (SEQ_UTENZA_FILIALI.NEXTVAL," & myReader1("ID_FILIALE") & "," & LBLID.Value & "," & myReader1("TUTTO_PATRIMONIO") & ")"
                par.cmd.ExecuteNonQuery()
            Loop
            myReader1.Close()

            par.myTrans.Commit()

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            MessJQuery("L\'Anagrafe Utenza è stata APERTA. E\' ora necessario inserire i relativi modelli di documentazione. Si ricorda che sono state automaticamente associate le filiali di default (AMMINISTRATIVE, AUCM, ETC)", 0, "Attenzione")
            BindGrid()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnChiudi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnChiudi.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            par.cmd.CommandText = "UPDATE utenza_BANDI SET STATO=2 WHERE ID=" & LBLID.Value
            par.cmd.ExecuteNonQuery()
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            MessJQuery("L\'Anagrafe Utenza è stata CHIUSA.", 0, "Attenzione")
            BindGrid()

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub rdbSolo0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbSolo0.CheckedChanged
        Cerca()
    End Sub

    Protected Sub rdbSolo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbSolo.CheckedChanged
        CercaAperte()
    End Sub



    Protected Sub btnEliminaBando_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaBando.Click
        Try

  
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            Dim DESCRIZIONE As String = ""

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE ID=" & LBLID.Value
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                DESCRIZIONE = par.IfNull(myReader1("DESCRIZIONE"), "")
            End If
            myReader1.Close()

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.POSTALER WHERE ID_TIPO_LETTERA=4 AND ID_LETTERA IN (SELECT ID from siscom_mi.CONVOCAZIONI_AU_LETTERE where id_convocazione in (select id FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & LBLID.Value & ")))"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "delete from siscom_mi.CONVOCAZIONI_AU_LETTERE where id_convocazione in (select id FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & LBLID.Value & "))"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI WHERE ID_APPUNTAMENTO IN (SELECT ID FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_AU=" & LBLID.Value & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_AU=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.AGENDA_PARAMETRI_OP WHERE ID_AU=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.CONVOCAZIONI_AU_EVENTI WHERE ID_CONVOCAZIONE IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & LBLID.Value & "))"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & LBLID.Value & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM UTENZA_CHIUSURE_PR WHERE ID_FILIALE IN (SELECT ID FROM UTENZA_FILIALI WHERE ID_BANDO=" & LBLID.Value & ")"
            par.cmd.ExecuteNonQuery()



            par.cmd.CommandText = "DELETE FROM UTENZA_LISTE_CDETT WHERE ID_LISTA IN (SELECT ID_LISTA FROM UTENZA_LISTE_CONV WHERE ID_AU=" & LBLID.Value & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM UTENZA_GRUPPI_CONV WHERE ID_AU=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM UTENZA_LISTE WHERE ID NOT IN (SELECT ID_LISTA_CONV FROM UTENZA_LISTE_CDETT)"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM UTENZA_LISTE_CONV WHERE ID_AU=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM UTENZA_SPORTELLI_PATRIMONIO WHERE ID_AU=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM UTENZA_OPERATORI WHERE ID_SPORTELLO IN (SELECT ID FROM UTENZA_SPORTELLI WHERE ID_FILIALE IN (SELECT ID FROM UTENZA_FILIALI WHERE ID_BANDO=" & LBLID.Value & "))"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM UTENZA_SPORTELLI WHERE ID_FILIALE IN (SELECT ID FROM UTENZA_FILIALI WHERE ID_BANDO=" & LBLID.Value & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM UTENZA_FILIALI WHERE ID_BANDO=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE siscom_mi.eventi_contratti SET MOTIVAZIONE=MOTIVAZIONE||' - ANNULLATA' where cod_evento='F221' AND MOTIVAZIONE='" & par.PulisciStrSql(DESCRIZIONE) & "'"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM UTENZA_TIPO_DOC WHERE ID_BANDO=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM UTENZA_BANDI WHERE ID=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            MessJQuery("Operazione effettuata!", 0, "Info")
            BindGrid()
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
