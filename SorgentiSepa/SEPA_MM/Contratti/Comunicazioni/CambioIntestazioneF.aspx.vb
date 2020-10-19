Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_Comunicazioni_CambioIntestazione
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Public Property IndiceContratto() As String
        Get
            If Not (ViewState("par_IndiceContratto") Is Nothing) Then
                Return CStr(ViewState("par_IndiceContratto"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceContratto") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim NomeFile As String = ""
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                IndiceContratto = Request.QueryString("IDC")
                Dim NomeFile1 As String = IndiceContratto & "_" & Format(Now, "yyyyMMddHHmmss")


                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\CambioIntestazione.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()

                par.cmd.CommandText = "select comuni_nazioni.nome as comune,anagRafica.* from comuni_nazioni,siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where anagrafica.cod_comune_nascita=comuni_nazioni.cod (+) and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.id_contratto=" & IndiceContratto & " and soggetti_contrattuali.cod_tipologia_occupante='INTE'"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Dim soggetto As String = ""

                If myReaderA.Read Then
                    If par.IfNull(myReaderA("ragione_sociale"), "") <> "" Then
                        soggetto = par.IfNull(myReaderA("ragione_sociale"), "") & " partita iva " & par.IfNull(myReaderA("partita_iva"), "") & " con sede legale in " & par.IfNull(myReaderA("comune_residenza"), "") & " " & par.IfNull(myReaderA("provincia_residenza"), "") & " " & par.IfNull(myReaderA("indirizzo_residenza"), "") & ", " & par.IfNull(myReaderA("civico_residenza"), "") & " CAP " & par.IfNull(myReaderA("cap_residenza"), "") & " "
                    End If

                    If par.IfNull(myReaderA("cognome"), "") <> "" Then
                        If soggetto <> "" Then
                            soggetto = soggetto & " rappresentato, "
                        End If
                        If par.IfNull(myReaderA("tipo_r"), "0") = "0" And par.IfNull(myReaderA("ragione_sociale"), "") <> "" Then
                            soggetto = soggetto & " in qualità di rappresentante legale, da "
                        End If
                        If par.IfNull(myReaderA("tipo_r"), "0") = "1" And par.IfNull(myReaderA("ragione_sociale"), "") <> "" Then
                            soggetto = soggetto & " in qualità di procuratore, numero procura " & par.IfNull(myReaderA("num_procura"), "") & " rilasciata in data " & par.FormattaData(par.IfNull(myReaderA("data_procura"), "")) & ", da "
                        End If
                        soggetto = soggetto & par.IfNull(myReaderA("cognome"), "") & " " & par.IfNull(myReaderA("nome"), "") & ", Codice Fiscale " & par.IfNull(myReaderA("cod_fiscale"), "") & ", nato a " & par.IfNull(myReaderA("comune"), "") & " il " & par.FormattaData(par.IfNull(myReaderA("data_nascita"), "")) & " "
                    End If


                End If
                myReaderA.Close()
                contenuto = Replace(contenuto, "$soggetto$", soggetto)

                par.cmd.CommandText = "select comuni_nazioni.nome as comune,anagRafica.* from comuni_nazioni,siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where anagrafica.cod_comune_nascita=comuni_nazioni.cod (+) and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.id_contratto=" & IndiceContratto & " and soggetti_contrattuali.cod_tipologia_occupante='EXINTE' ORDER BY SOGGETTI_CONTRATTUALI.DATA_FINE DESC"
                myReaderA = par.cmd.ExecuteReader()
                Dim vecchiosoggetto As String = ""

                If myReaderA.Read Then
                    If par.IfNull(myReaderA("ragione_sociale"), "") <> "" Then
                        vecchiosoggetto = par.IfNull(myReaderA("ragione_sociale"), "") & " partita iva " & par.IfNull(myReaderA("partita_iva"), "")
                    End If
                    If par.IfNull(myReaderA("cognome"), "") <> "" Then
                        If vecchiosoggetto <> "" Then
                            vecchiosoggetto = vecchiosoggetto & " rappresentato da "
                        End If
                        vecchiosoggetto = vecchiosoggetto & par.IfNull(myReaderA("cognome"), "") & " " & par.IfNull(myReaderA("nome"), "") & ", Codice Fiscale " & par.IfNull(myReaderA("cod_fiscale"), "") & ", nato a " & par.IfNull(myReaderA("comune"), "") & " il " & par.FormattaData(par.IfNull(myReaderA("data_nascita"), "")) & " e residente per la carica in " & par.IfNull(myReaderA("comune_residenza"), "") & " " & par.IfNull(myReaderA("indirizzo_residenza"), "") & ", " & par.IfNull(myReaderA("civico_residenza"), "") & " "
                    End If

                End If
                myReaderA.Close()
                contenuto = Replace(contenuto, "$vecchiointestatario$", vecchiosoggetto)


                Dim datialloggio As String = ""
                Dim CodiceContratto As String = ""

                par.cmd.CommandText = "SELECT TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",COMUNI_NAZIONI.SIGLA,COMUNI_NAZIONI.NOME,UNITA_CONTRATTUALE.*,RAPPORTI_UTENZA.DEST_USO,rapporti_utenza.cod_contratto FROM SISCOM_MI.TIPO_LIVELLO_PIANO,COMUNI_NAZIONI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA WHERE TIPO_LIVELLO_PIANO.COD=UNITA_CONTRATTUALE.COD_TIPO_LIVELLO_PIANO AND COMUNI_NAZIONI.COD=UNITA_CONTRATTUALE.COD_COMUNE AND RAPPORTI_UTENZA.id='" & IndiceContratto & "' AND  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    CodiceContratto = par.IfNull(myReaderA("cod_contratto"), "")
                    datialloggio = "Codice " & par.IfNull(myReaderA("cod_unita_immobiliare"), "") & " " & par.IfNull(myReaderA("INDIRIZZO"), "") & ", " & par.IfNull(myReaderA("CIVICO"), "")
                    If par.IfNull(myReaderA("PIANO"), "//") <> "" Then
                        datialloggio = datialloggio & " piano " & par.IfNull(myReaderA("PIANO"), "//")
                    End If

                    If par.IfNull(myReaderA("scala"), "") <> "" Then
                        datialloggio = datialloggio & " scala " & par.IfNull(myReaderA("scala"), "//")
                    End If

                    If par.IfNull(myReaderA("interno"), "") <> "" Then
                        datialloggio = datialloggio & " interno " & par.IfNull(myReaderA("interno"), "//")
                    End If

                    datialloggio = datialloggio & " comune di " & par.IfNull(myReaderA("NOME"), "//") & " (" & par.IfNull(myReaderA("SIGLA"), "//") & ")"


                End If
                myReaderA.Close()

                contenuto = Replace(contenuto, "$datialloggio$", datialloggio)
                contenuto = Replace(contenuto, "$codicecontratto$", CodiceContratto)


                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeLettere\CambioI_") & NomeFile1 & ".htm", False, System.Text.Encoding.Default)
                sr.WriteLine(contenuto)
                sr.Close()


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>window.open('../../ALLEGATI/CONTRATTI/StampeLettere/CambioI_" & NomeFile1 & ".htm','CambioIntestazioneS','');self.close();</script>")



            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:Cambio Intestazione" & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            End Try
        End If
    End Sub
End Class
