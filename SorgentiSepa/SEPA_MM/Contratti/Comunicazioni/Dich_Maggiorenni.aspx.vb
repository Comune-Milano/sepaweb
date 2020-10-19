Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_Comunicazioni_Dich_Maggiorenni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Dim NomeFile As String
            Dim ELENCOCOMPONENTI As String = ""
            Dim testotabella As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\Dich_Maggiorenni.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            par.cmd.CommandText = "SELECT anagrafica.cognome, anagrafica.nome FROM siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza WHERE rapporti_utenza.cod_contratto ='" & Request.QueryString("COD") & "' AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' AND anagrafica.ID = soggetti_contrattuali.id_anagrafica"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$cognome$", par.IfNull(myReader("COGNOME"), ""))
                contenuto = Replace(contenuto, "$nome$", par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT unita_contrattuale.* FROM siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza WHERE rapporti_utenza.cod_contratto = '" & Request.QueryString("COD") & "' AND  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND unita_contrattuale.id_contratto = rapporti_utenza.ID"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("INDIRIZZO"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO"), "__________________"))
                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReader("INTERNO"), "__________________"))
                contenuto = Replace(contenuto, "$comune$", par.IfNull(myReader("LOCALITA"), "__________________"))
                contenuto = Replace(contenuto, "$codiceunita$", par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), "__________________"))

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT anagrafica.*,tipologia_parentela.descrizione as parente FROM siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza,siscom_mi.tipologia_parentela WHERE soggetti_contrattuali.cod_tipologia_parentela =tipologia_parentela.cod and rapporti_utenza.cod_contratto ='" & Request.QueryString("COD") & "' AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID AND anagrafica.ID = soggetti_contrattuali.id_anagrafica order by anagrafica.data_nascita asc"
            Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader123.Read
                If par.RicavaEta(par.IfNull(myReader123("DATA_NASCITA"), Format(Now, "yyyyMMdd"))) >= 18 Then
                    ELENCOCOMPONENTI = ELENCOCOMPONENTI & "<tr><td align='left' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>" & par.IfNull(myReader123("COGNOME"), "") & " " & par.IfNull(myReader123("NOME"), "") & "</td>" _
                    & "<td align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader123("PARENTE"), "") & "</td>" _
                    & "<td align='left' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>&nbsp</td></tr>"
                End If
            Loop
            myReader123.Close()

            testotabella = "<table style='border: thin solid #000000; width: 100%; border-collapse: collapse;'><tr>" _
            & "<td align='center' style='font-weight: bold;border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>Cognome e nome</td>" _
            & "<td align='center' style='font-weight: bold;border-width: thin; border-color: #000000; border-style: none solid solid solid;'>Grado di parentela</td>" _
            & "<td align='center' style='font-weight: bold;border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>Firma di accettazione</td></tr>"

            ELENCOCOMPONENTI = testotabella & ELENCOCOMPONENTI & "</table>"

            contenuto = Replace(contenuto, "$elencocomponenti$", ELENCOCOMPONENTI)

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
            Session.Add("ERRORE", "Provenienza:Lettera Maggiorenni" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
