Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_Comunicazioni_Normativa_Privacy
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Dim NomeFile As String
            Dim IdContratto As Long = 0
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\Normativa_Privacy.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            par.cmd.CommandText = "SELECT anagrafica.cognome, anagrafica.nome FROM siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza WHERE rapporti_utenza.cod_contratto ='" & Request.QueryString("COD") & "' AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' AND anagrafica.ID = soggetti_contrattuali.id_anagrafica"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$cognome$", par.IfNull(myReader("COGNOME"), ""))
                contenuto = Replace(contenuto, "$nome$", par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & Request.QueryString("COD") & "'"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                IdContratto = par.IfNull(myReader2("ID"), "")
            End If
            myReader2.Close()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & IdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F90','Normativa Privacy')"
            par.cmd.ExecuteNonQuery()


            Dim PercorsoBarCode As String = par.RicavaBarCode(21, IdContratto)
            contenuto = Replace(contenuto, "$barcode$", "..\..\..\FileTemp\" & PercorsoBarCode)


            NomeFile = Format(Now, "yyyyMMddHHmmss")
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeLettere\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()

            Response.Redirect("..\..\ALLEGATI\CONTRATTI\StampeLettere\" & NomeFile & ".htm")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Lettera Privacy" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
