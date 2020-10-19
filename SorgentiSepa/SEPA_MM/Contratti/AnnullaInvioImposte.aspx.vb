
Partial Class Contratti_AnnullaInvioImposte
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            lblTitolo.Text = "REPORT ANNULLO INVIO FILE " & UCase(par.DeCripta(Request.QueryString("ID")))
            idContratto = Request.QueryString("IDRU")
            ProceduraAnnullo(par.DeCripta(Request.QueryString("ID")))
        End If
    End Sub

    Public Property idContratto() As Long
        Get
            If Not (ViewState("par_idContratto") Is Nothing) Then
                Return CLng(ViewState("par_idContratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idContratto") = value
        End Set

    End Property

    Private Sub ProceduraAnnullo(ByVal NomeFile As String)
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()

            lblTabella.Text = ""
            par.cmd.CommandText = "select rapporti_utenza.id as idc,ID_FASE_REGISTRAZIONE,RAPPORTI_UTENZA.COD_CONTRATTO,REPLACE(SISCOM_MI.GETINTESTATARI(RAPPORTI_UTENZA.ID),';','') AS INTESTATARIO, COD_TRIBUTO,ANNO,(case when length(RAPPORTI_UTENZA_IMPOSTE.DATA_CREAZIONE)=8 then to_char(to_date(RAPPORTI_UTENZA_IMPOSTE.DATA_CREAZIONE,'yyyymmdd'),'dd/mm/yyyy') else to_char(to_date(substr(RAPPORTI_UTENZA_IMPOSTE.DATA_CREAZIONE,1,8),'yyyymmdd'),'dd/mm/yyyy')||' - '||substr(RAPPORTI_UTENZA_IMPOSTE.DATA_CREAZIONE,9,2)||':'||substr(RAPPORTI_UTENZA_IMPOSTE.DATA_CREAZIONE,11,2) end) AS DATA_CREAZIONE,to_char(to_date(RAPPORTI_UTENZA_IMPOSTE.DATA_AE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_INVIO,IMPORTO_CANONE,IMPORTO_TRIBUTO,IMPORTO_SANZIONE,IMPORTO_INTERESSI from SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE,SISCOM_MI.RAPPORTI_UTENZA where RAPPORTI_UTENZA.ID=RAPPORTI_UTENZA_IMPOSTE.ID_CONTRATTO and id_Contratto=" & idContratto & " AND UPPER(file_scaricato)='" & UCase(NomeFile) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                If par.IfNull(myReader("ID_FASE_REGISTRAZIONE"), "") = "3" Then
                    lblTabella.Text = lblTabella.Text & "<tr><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("COD_CONTRATTO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("INTESTATARIO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("COD_TRIBUTO"), "") & "</td><td  style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("ANNO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("DATA_CREAZIONE"), "") & "</td><td  style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("DATA_INVIO"), "") & "</td><td  style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_CANONE"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_TRIBUTO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_SANZIONE"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_INTERESSI"), "") & "</td><td  style='font-family: arial; font-size: 9pt'><img src='../nuoveimm/Stop-icon.png'/>Ricevuta già annullata</td></tr>"
                End If
                If par.IfNull(myReader("ID_FASE_REGISTRAZIONE"), "") = "2" Then
                    lblTabella.Text = lblTabella.Text & "<tr><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("COD_CONTRATTO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("INTESTATARIO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("COD_TRIBUTO"), "") & "</td><td  style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("ANNO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("DATA_CREAZIONE"), "") & "</td><td  style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("DATA_INVIO"), "") & "</td><td  style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_CANONE"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_TRIBUTO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_SANZIONE"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_INTERESSI"), "") & "</td><td  style='font-family: arial; font-size: 9pt'><img src='../nuoveimm/Stop-icon.png'/>Ricevuta già inserita</td></tr>"
                End If
                If par.IfNull(myReader("ID_FASE_REGISTRAZIONE"), "") = "1" Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE SET ID_FASE_REGISTRAZIONE=3,NOTE='ANNULLO FILE DA OPERATORE' WHERE ID_FASE_REGISTRAZIONE=1 AND COD_TRIBUTO ='" & par.IfNull(myReader("COD_TRIBUTO"), "") & "' AND ANNO='" & par.IfNull(myReader("ANNO"), "") & "' AND ID_CONTRATTO=" & par.IfNull(myReader("IDC"), "")
                    par.cmd.ExecuteNonQuery()
                    lblTabella.Text = lblTabella.Text & "<tr><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("COD_CONTRATTO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("INTESTATARIO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("COD_TRIBUTO"), "") & "</td><td  style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("ANNO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("DATA_CREAZIONE"), "") & "</td><td  style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("DATA_INVIO"), "") & "</td><td  style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_CANONE"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_TRIBUTO"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_SANZIONE"), "") & "</td><td style='font-family: arial; font-size: 9pt'>" & par.IfNull(myReader("IMPORTO_INTERESSI"), "") & "</td><td  style='font-family: arial; font-size: 9pt'><img src='../nuoveimm/Abbina_Seleziona.png'/>Annullato</td></tr>"
                End If
            Loop
            myReader.Close()
            lblTabella.Text = lblTabella.Text & "</tr></table>"
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE_ANN VALUES (SISCOM_MI.SEQ_RU_IMPOSTE_ANN.NEXTVAL,'" & UCase(NomeFile) & "','" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ")"
            par.cmd.ExecuteNonQuery()
            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>if (opener.document.getElementById('btnAggiorna')){opener.document.getElementById('btnAggiorna').click();}</script>")


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
