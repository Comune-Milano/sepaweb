Imports System.IO

Partial Class Contratti_CONTRATTI_LIGHT_DettagliCalcolo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE_AU_LIGHT") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            If Request.QueryString("File") <> "" Then
                Dim fileName As String = Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeCanoni27\" & Request.QueryString("File"))

                If File.Exists(fileName) = True Then
                    Dim objStreamReader As StreamReader
                    objStreamReader = File.OpenText(fileName)
                    Dim contenuto As String = objStreamReader.ReadToEnd()
                    LBLTESTO.Text = Replace(contenuto, vbCrLf, "<br />")
                    objStreamReader.Close()
                Else
                    Response.Write("File non trovato! Potrebbe essere stato rimosso dal server!")
                End If
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                Dim indirizzo As String = ""

                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\ProspettoCanone.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_EC WHERE ID_CONTRATTO=" & Request.QueryString("IDC") & " AND DATA_CALCOLO='" & Request.QueryString("DATA") & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,siscom_mi.rapporti_utenza WHERE RAPPORTI_UTENZA.COD_CONTRATTO='" & myReader("cod_contratto") & "' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE') AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA ORDER BY SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE DESC"
                    Dim myReader345 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader345.Read Then
                        contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader345("INTESTATARIO"), ""))
                    End If
                    myReader345.Close()
                    par.cmd.CommandText = "select unita_immobiliari.cod_unita_immobiliare,unita_immobiliari.id_indirizzo from siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale where unita_immobiliari.id=unita_contrattuale.id_unita and unita_contrattuale.id_unita_principale is null and id_contratto=" & myReader("id_contratto")
                    myReader345 = par.cmd.ExecuteReader()
                    If myReader345.Read Then
                        contenuto = Replace(contenuto, "$codicealloggio$", par.IfNull(myReader345("cod_unita_immobiliare"), ""))
                        indirizzo = par.IfNull(myReader345("id_indirizzo"), "-1")
                    End If
                    myReader345.Close()

                    par.cmd.CommandText = "select * from siscom_mi.indirizzi where id=" & indirizzo
                    myReader345 = par.cmd.ExecuteReader()
                    If myReader345.Read Then
                        contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader345("descrizione"), "") & " " & par.IfNull(myReader345("civico"), "") & " " & par.IfNull(myReader345("cap"), "") & " " & par.IfNull(myReader345("localita"), ""))
                    End If
                    myReader345.Close()
                    contenuto = Replace(contenuto, "$datacalcolo$", par.FormattaData(Mid(par.IfNull(myReader("data_calcolo"), ""), 1, 8)))
                    contenuto = Replace(contenuto, "$codicecontratto$", par.IfNull(myReader("cod_contratto"), ""))
                    contenuto = Replace(contenuto, "$demografia$", par.IfNull(myReader("dem"), ""))
                    contenuto = Replace(contenuto, "$supconv$", par.IfNull(myReader("supconvenzionale"), ""))
                    contenuto = Replace(contenuto, "$costobase$", par.IfNull(myReader("costobase"), ""))
                    contenuto = Replace(contenuto, "$ubicazione$", par.IfNull(myReader("zona"), ""))
                    contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("piano"), ""))
                    contenuto = Replace(contenuto, "$ascensore$", "")
                    contenuto = Replace(contenuto, "$conservazione$", par.IfNull(myReader("conservazione"), ""))
                    contenuto = Replace(contenuto, "$vetusta$", par.IfNull(myReader("vetusta"), ""))
                    contenuto = Replace(contenuto, "$convenzionale$", Format((par.IfNull(myReader("valore_locativo"), "0") * 100) / 5, "##,##0.00"))
                    contenuto = Replace(contenuto, "$locativo$", Format(CDbl(par.IfNull(myReader("valore_locativo"), "")), "##,##0.00"))

                    Select Case par.IfNull(myReader("id_area_economica"), "")
                        Case "1"
                            contenuto = Replace(contenuto, "$area$", "PROTEZIONE")
                        Case "2"
                            contenuto = Replace(contenuto, "$area$", "ACCESSO")
                        Case "3"
                            contenuto = Replace(contenuto, "$area$", "PERMANENZA")
                        Case "4"
                            contenuto = Replace(contenuto, "$area$", "DECADENZA")
                    End Select

                    If par.IfNull(myReader("id_area_economica"), "") <> "4" Then
                        contenuto = Replace(contenuto, "$ncomp$", par.IfNull(myReader("num_comp"), ""))
                        contenuto = Replace(contenuto, "$ncomp15$", par.IfNull(myReader("minori_15"), ""))
                        contenuto = Replace(contenuto, "$ncomp65$", par.IfNull(myReader("maggiori_65"), ""))
                        contenuto = Replace(contenuto, "$ncomp6699$", par.IfNull(myReader("num_comp_66"), ""))
                        contenuto = Replace(contenuto, "$ncomp100$", par.IfNull(myReader("num_comp_100"), ""))
                        contenuto = Replace(contenuto, "$ncomp100i$", par.IfNull(myReader("num_comp_100_con"), ""))
                        contenuto = Replace(contenuto, "$detrazioni$", Format(CDbl(par.IfNull(myReader("detrazioni"), 0)), "0.00"))
                        contenuto = Replace(contenuto, "$fragilita$", Format(CDbl(par.IfNull(myReader("detrazioni_fragilita"), 0)), "0.00"))
                        contenuto = Replace(contenuto, "$mobiliare$", Format(CDbl(par.IfNull(myReader("redd_mobiliari"), 0)), "0.00"))
                        contenuto = Replace(contenuto, "$mobiliare$", Format(CDbl(par.IfNull(myReader("redd_mobiliari"), 0)), "0.00"))
                        contenuto = Replace(contenuto, "$immobiliare$", Format(CDbl(par.IfNull(myReader("redd_immobiliari"), 0)), "0.00"))
                        contenuto = Replace(contenuto, "$complessivo$", Format(CDbl(par.IfNull(myReader("redd_complessivo"), 0)), "0.00"))
                        contenuto = Replace(contenuto, "$isee$", par.IfNull(myReader("isee"), ""))
                        contenuto = Replace(contenuto, "$ise$", par.IfNull(myReader("ise"), ""))
                        contenuto = Replace(contenuto, "$isr$", par.IfNull(myReader("isr"), ""))
                        contenuto = Replace(contenuto, "$isp$", par.IfNull(myReader("isp"), ""))
                        contenuto = Replace(contenuto, "$vse$", par.IfNull(myReader("vse"), ""))
                        contenuto = Replace(contenuto, "$redddip$", Format(CDbl(par.IfNull(myReader("redditi_dip"), 0)), "0.00"))
                        contenuto = Replace(contenuto, "$reddaltri$", Format(CDbl(par.IfNull(myReader("redditi_atri"), 0)), "0.00"))
                        contenuto = Replace(contenuto, "$limite$", par.IfNull(myReader("limite_pensioni"), ""))
                        If par.IfNull(myReader("redd_prev_dip"), "0") = "0" Then
                            contenuto = Replace(contenuto, "$prevalente$", "NO")
                        Else
                            contenuto = Replace(contenuto, "$prevalente$", "SI")
                        End If

                    Else
                        contenuto = Replace(contenuto, "$ncomp$", "---")
                        contenuto = Replace(contenuto, "$ncomp15$", "---")
                        contenuto = Replace(contenuto, "$ncomp65$", "---")
                        contenuto = Replace(contenuto, "$ncomp6699$", "---")
                        contenuto = Replace(contenuto, "$ncomp100$", "---")
                        contenuto = Replace(contenuto, "$ncomp100i$", "---")
                        contenuto = Replace(contenuto, "$detrazioni$", "---")
                        contenuto = Replace(contenuto, "$fragilita$", "---")
                        contenuto = Replace(contenuto, "$mobiliare$", "---")
                        contenuto = Replace(contenuto, "$mobiliare$", "---")
                        contenuto = Replace(contenuto, "$immobiliare$", "---")
                        contenuto = Replace(contenuto, "$complessivo$", "---")
                        contenuto = Replace(contenuto, "$isee$", "---")
                        contenuto = Replace(contenuto, "$ise$", "---")
                        contenuto = Replace(contenuto, "$isr$", "---")
                        contenuto = Replace(contenuto, "$isp$", "---")
                        contenuto = Replace(contenuto, "$vse$", "---")
                        contenuto = Replace(contenuto, "$redddip$", "---")
                        contenuto = Replace(contenuto, "$reddaltri$", "---")
                        contenuto = Replace(contenuto, "$limite$", "---")

                        contenuto = Replace(contenuto, "$prevalente$", "---")

                        contenuto = Replace(contenuto, "$ise27$", "---")
                        contenuto = Replace(contenuto, "$incidenzalocativo$", "---")
                        contenuto = Replace(contenuto, "$incidenzaise$", "---")
                        contenuto = Replace(contenuto, "$incidenzalocativo$", "---")
                        contenuto = Replace(contenuto, "$coeffnuclei$", "---")
                    End If
                    contenuto = Replace(contenuto, "$fascia$", par.IfNull(myReader("SOTTO_AREA"), ""))


                    If Val(par.IfNull(myReader("isee"), "")) = Val(par.IfNull(myReader("ISEE_27"), "")) Then
                        contenuto = Replace(contenuto, "$isee27$", par.IfNull(myReader("ISEE"), ""))
                        contenuto = Replace(contenuto, "$ise27$", par.IfNull(myReader("ISE"), ""))
                    Else
                        contenuto = Replace(contenuto, "$isee27$", par.IfNull(myReader("ISEE_27"), ""))
                        contenuto = Replace(contenuto, "$ise27$", Format(par.IfNull(myReader("ISEE_27"), "") * par.IfNull(myReader("vse"), ""), "0.00"))
                    End If


                    contenuto = Replace(contenuto, "$perclocativo$", par.IfNull(myReader("PERC_VAL_LOC"), "") & "%")
                    contenuto = Replace(contenuto, "$incidenzalocativo$", par.IfNull(myReader("INC_MAX"), "") & "%")

                    contenuto = Replace(contenuto, "$incidenzaise$", par.IfNull(myReader("INCIDENZA_ISE"), ""))
                    contenuto = Replace(contenuto, "$coeffnuclei$", par.IfNull(myReader("COEFF_NUCLEO_FAM"), ""))
                    contenuto = Replace(contenuto, "$minimo$", Format(CDbl(par.IfNull(myReader("CANONE_MINIMO_AREA"), 0)), "0.00"))
                    contenuto = Replace(contenuto, "$canoneclasse$", Format(CDbl(par.IfNull(myReader("CANONE_CLASSE"), 0)), "0.00"))
                    contenuto = Replace(contenuto, "$istat$", par.IfNull(myReader("PERC_ISTAT_APPLICATA"), 0) & "%")
                    contenuto = Replace(contenuto, "$classeistat$", Format(CDbl(par.IfNull(myReader("CANONE_CLASSE_ISTAT"), 0)), "0.00"))
                    contenuto = Replace(contenuto, "$annuale$", Format(CDbl(par.IfNull(myReader("CANONE"), 0)), "0.00"))
                    contenuto = Replace(contenuto, "$mensile$", Format(Format(CDbl(par.IfNull(myReader("CANONE"), 0)), "0.00") / 12, "0.00"))

                    contenuto = Replace(contenuto, "$annotazioni$", par.IfNull(myReader("annotazioni"), ""))
                End If
                myReader.Close()
                par.OracleConn.Close()
                LBLTESTO.Text = contenuto
            End If

        End If
    End Sub
End Class
