Imports System
Imports System.Data
Imports System.IO
Imports Telerik.Web.UI

Partial Class Contratti_MarcaContrattiRegistrAE
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim Str As String = ""
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", True)
            Exit Sub
        End If

        Me.connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then
            txtDataInvio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataInvio.SelectedDate = Format(Now, "dd/MM/yyyy")
        End If
    End Sub
    
    Private Function UploadOnServer() As String
        UploadOnServer = ""

        Dim fileName As String = ""
        
        For Each file As UploadedFile In FileUpload1.UploadedFiles
            fileName = file.GetName()
            UploadOnServer = file.GetNameWithoutExtension() & "_" & Format(Now, "yyyyMMddHHmmss") & file.GetExtension
            UploadOnServer = Server.MapPath("..\FileTemp\") & UploadOnServer
            file.SaveAs(UploadOnServer)
        Next

        Return UploadOnServer
    End Function

    Protected Sub btnAllega_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllega.Click
        Try
            'If confermaProcedi.Value = "1" Then
            Dim errore As String = ""
            Dim totRU As Integer = 0
            Dim totRUEffettivi As Integer = 0
            Dim FileName As String = UCase(UploadOnServer())
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")
            If Not String.IsNullOrEmpty(FileName) Then
                If objFile.FileExists(FileName) And FileName.Contains(".TXT") Then
                    txtNote.Text = ""
                    connData.apri(True)
                    Dim sr = New StreamReader(FileName)
                    Dim codRU As String = ""
                    Do
                        codRU = sr.ReadLine
                        If codRU <> "" Then
                            par.cmd.CommandText = "SELECT id from siscom_mi.RAPPORTI_UTENZA where COD_CONTRATTO='" & Trim(codRU.ToUpper) & "' and not exists (select id_contratto from " _
                            & " siscom_mi.rapporti_utenza_ricevute where id_contratto=id) and bozza=0 and siscom_mi.getstatocontratto(id)='IN CORSO'"
                            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dt1 As New Data.DataTable
                            da1.Fill(dt1)
                            da1.Dispose()
                            totRU = totRU + 1
                            If dt1.Rows.Count > 0 Then
                                For Each rowDT In dt1.Rows
                                    Try
                                        par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where id = " & rowDT.item("ID") & " for update nowait"
                                        par.cmd.ExecuteNonQuery()

                                    Catch exLok As Oracle.DataAccess.Client.OracleException
                                        If exLok.Number = 54 Then
                                            connData.chiudi(False)
                                            txtNote.Text = "OPERAZIONE ANNULLATA!" & vbCrLf & "Il rapporto cod. " & codRU.ToUpper & " risulta essere aperto da altro operatore."
                                            Exit Sub
                                        End If
                                    End Try
                                    RimarcaContratti(rowDT.item("ID"))
                                    totRUEffettivi = totRUEffettivi + 1
                                    txtNote.Text &= "Il rapporto con identificativo " & codRU.ToUpper & " è stato aggiornato." & vbCrLf
                                Next
                            Else
                                txtNote.Text &= "Il rapporto con identificativo " & codRU.ToUpper & " non è valido. Aggiornamento non riuscito." & vbCrLf
                            End If

                        End If
                    Loop Until codRU Is Nothing
                    sr.Close()

                    If totRUEffettivi > 1 Then
                        txtNote.Text &= "OPERAZIONE COMPLETATA!" & vbCrLf & "Sono stati aggiornati " & totRUEffettivi & "/" & totRU & " contratti."
                    Else
                        txtNote.Text &= "OPERAZIONE COMPLETATA!" & vbCrLf & "E' stato aggiornato " & totRUEffettivi & " contratto."
                    End If
                    connData.chiudi(True)
                Else
                    txtNote.Text = "ERRORE! Tipo file non valido. Selezionare un file txt."
                End If
            Else
                txtNote.Text = "ERRORE! Selezionare un file txt contenente l'elenco dei contratti da rimarcare."
            End If
            ' End If
        Catch ex As Exception
            connData.chiudi(False)
            txtNote.Text = txtNote.Text & vbCrLf & ex.Message & vbCrLf & "OPERAZIONE ANNULLATA!"
        End Try
    End Sub

    Private Sub RimarcaContratti(ByVal idContratto As Long)
        Dim dataAE As String = par.AggiustaData(CDate(txtDataInvio.SelectedDate).ToShortDateString)

        par.cmd.CommandText = "update siscom_mi.rapporti_utenza_imposte set id_fase_registrazione=3,note='Registrazione annullata massivamente da procedura' where id_contratto=" & idContratto
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "update siscom_mi.rapporti_utenza set data_decorrenza_ae='" & dataAE & "'," _
            & " data_scadenza=to_char(ADD_MONTHS(TO_DATE('" & dataAE & "','yyyyMMdd'), durata_anni*12),'yyyyMMdd')," _
            & " data_scadenza_rinnovo=to_char(ADD_MONTHS(TO_DATE('" & dataAE & "','yyyyMMdd'), (durata_anni+durata_rinnovo)*12),'yyyyMMdd')," _
            & " reg_telematica='' where id=" & idContratto
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "INSERT INTO siscom_mi.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                   & "VALUES (" & idContratto & "," & Session("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                   & "'F02','Rimarcatura contratto per registrazione AE')"
        par.cmd.ExecuteNonQuery()
    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "function PaginaHome() {document.location.href = 'pagina_home.aspx';};PaginaHome();", True)
    End Sub

End Class
