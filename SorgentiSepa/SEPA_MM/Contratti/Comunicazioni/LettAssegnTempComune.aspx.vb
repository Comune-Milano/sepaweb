Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_Comunicazioni_LettAssegnTempComune
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Dim NomeFile As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\Documenti_AssegnTemp\letteraTempComune.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            par.cmd.CommandText = "SELECT anagrafica.cognome, anagrafica.nome FROM siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza WHERE rapporti_utenza.cod_contratto ='" & Request.QueryString("COD") & "' AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' AND anagrafica.ID = soggetti_contrattuali.id_anagrafica"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT unita_contrattuale.*,rapporti_utenza.data_scadenza,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,COMUNI_NAZIONI.NOME AS CITTA FROM SISCOM_MI.TIPO_LIVELLO_PIANO,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza,COMUNI_NAZIONI WHERE rapporti_utenza.cod_contratto = '" & Request.QueryString("COD") & "' AND COMUNI_NAZIONI.COD=UNITA_CONTRATTUALE.COD_COMUNE AND TIPO_LIVELLO_PIANO.COD=UNITA_CONTRATTUALE.COD_TIPO_LIVELLO_PIANO AND unita_contrattuale.id_contratto = rapporti_utenza.ID and id_unita_principale is null"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("SCALA"), ""))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$dataScadenza$", par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("INDIRIZZO"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReader("INTERNO"), "__________________"))
                contenuto = Replace(contenuto, "$comune$", par.IfNull(myReader("CITTA"), "__________________"))
            End If
            myReader.Close()

            par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.tab_filiali,siscom_mi.indirizzi where indirizzi.id = tab_filiali.id_indirizzo and tab_filiali.id = 12"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
                contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
                contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
                contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
                contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))

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
            Session.Add("ERRORE", "Provenienza:Lettera Temp." & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
