Imports System.IO

Partial Class Contratti_Comunicazioni_ModuloRifiuti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Dim NomeFile As String
            Dim idContratto As Long = 0
            Dim personaGiuridica As Boolean = False

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\ModuloTassaRifiuti.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            par.cmd.CommandText = "SELECT anagrafica.*,soggetti_contrattuali.ID_CONTRATTO,rapporti_utenza.data_stipula FROM siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza WHERE rapporti_utenza.cod_contratto ='" & Request.QueryString("COD") & "' AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' AND anagrafica.ID = soggetti_contrattuali.id_anagrafica"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                idContratto = par.IfNull(myReader("ID_CONTRATTO"), "")
                contenuto = Replace(contenuto, "$cognome$", par.IfNull(myReader("COGNOME"), ""))
                contenuto = Replace(contenuto, "$nome$", par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$dataStipula$", par.FormattaData(par.IfNull(myReader("DATA_STIPULA"), "")))
                If par.IfNull(myReader("RAGIONE_SOCIALE"), "") <> "" Then
                    personaGiuridica = True
                End If
            End If
            myReader.Close()

            Dim indirizzoRecapito As String = ""
            Dim comuneNasc As String = ""
            Dim provNasc As String = ""
            Dim tblPersona As String = "<table style='width: 100%; border-top-style: solid; border-top-width: 1px; border-top-color: #000000;border-collapse: collapse;'>"

            par.cmd.CommandText = "SELECT anagrafica.* FROM siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza WHERE rapporti_utenza.cod_contratto ='" & Request.QueryString("COD") & "' AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' AND anagrafica.ID = soggetti_contrattuali.id_anagrafica"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If personaGiuridica = True Then
                    tblPersona = tblPersona & "<tr><td align='center' style='width: 10px; border-right-style: solid; border-right-width: 1px;border-right-color: #000000; border-left-style: solid; border-left-width: 1px;border-left-color: #000000; border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000;'><img alt='' src='ImgPersonaGiuridica.jpg' /></td>" _
                        & "<td align='justify' style='padding-left: 10px; border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000; border-right-style: solid; border-right-width: 1px;border-right-color: #000000;'>" _
                        & "<b>RAGIONE SOCIALE</b> " & par.IfNull(myReader("RAGIONE_SOCIALE"), "") & "<br />" _
                        & "Con Sede a " & par.IfNull(myReader("COMUNE_RESIDENZA"), "") & "<br />" _
                        & "Cod. fiscale " & par.IfNull(myReader("COD_FISCALE"), "-----") & " P.IVA " & par.IfNull(myReader("PARTITA_IVA"), "") & "<br />" _
                        & "Telefono n." & par.IfNull(myReader("TELEFONO"), "") & "<br />" _
                        & "P.E.C. (Posta Elettronica Certificata) ___________________________________________________________________<br />" _
                        & "<b>Nominativo del Rappresentante legale</b><br />" _
                        & "In qualità di <br />" _
                        & "Cod. fiscale <br />" _
                        & "Nato a <br />" _
                        & "(Prov.   ) il <br />" _
                        & "Residente a Via n.</td></tr>"
                Else
                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & par.IfNull(myReader("COD_COMUNE_NASCITA"), "") & "'"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        comuneNasc = par.IfNull(myReader2("NOME"), "")
                        provNasc = par.IfNull(myReader2("SIGLA"), "")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * from siscom_mi.indirizzi_anagrafica where id =" & par.IfNull(myReader("ID_INDIRIZZO_RECAPITO"), 0)
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        indirizzoRecapito = par.IfNull(myReader3("DESCRIZIONE"), "") & ", " & par.IfNull(myReader3("CIVICO"), "") & " - " & par.IfNull(myReader3("LOCALITA"), "")
                    End If
                    myReader3.Close()

                    tblPersona = tblPersona & "<tr><td align='center' style='width: 10px; border-right-style: solid; border-right-width: 1px;border-right-color: #000000; border-left-style: solid; border-left-width: 1px;border-left-color: #000000; border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000;'><img alt='' src='ImgPersonaFisica.jpg' /></td>" _
                        & "<td align='justify' style='padding-left: 10px; border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000; border-right-style: solid; border-right-width: 1px;border-right-color: #000000;'>" _
                        & "<b>COGNOME/NOME</b> " & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "<br />" _
                        & "Nato a " & comuneNasc & " Prov. (" & provNasc & ") Il " & par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")) & "<br />" _
                        & "Residente a " & par.IfNull(myReader("COMUNE_RESIDENZA"), "") & " " & par.IfNull(myReader("INDIRIZZO_RESIDENZA"), "") & " N. " & par.IfNull(myReader("CIVICO_RESIDENZA"), "") & "<br />" _
                        & "Cod. fiscale " & par.IfNull(myReader("COD_FISCALE"), "") & "<br />" _
                        & "Indirizzo di recapito " & indirizzoRecapito & "<br />" _
                        & "Telefono n." & par.IfNull(myReader("TELEFONO"), "") & "<br />" _
                        & "P.E.C. (Posta Elettronica Certificata) ___________________________________________________________________ " _
                        & "</td></tr>"
                End If
            End If
            myReader.Close()

            tblPersona = tblPersona & "</table>"

            contenuto = Replace(contenuto, "$tabellaPersona$", tblPersona)

            par.cmd.CommandText = "SELECT unita_contrattuale.* from siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza where rapporti_utenza.cod_contratto = '" & Request.QueryString("COD") & "' AND  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND unita_contrattuale.id_contratto = rapporti_utenza.ID"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("INDIRIZZO"), "__________________"))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO"), "__________________"))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT COUNT(ID_ANAGRAFICA) AS NUMCOMP FROM siscom_mi.soggetti_contrattuali where NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "' and id_contratto=" & idContratto
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("NUMCOMP"), 0) > 1 Then
                    contenuto = Replace(contenuto, "$imgFlag$", "<img src='../../../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../../../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                Else
                    contenuto = Replace(contenuto, "$imgFlag$", "<img src='../../../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../../../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
                End If
            End If
            myReader.Close()

            Dim tblComponenti As String = "<table>"
            par.cmd.CommandText = "SELECT anagrafica.* FROM siscom_mi.soggetti_contrattuali,siscom_mi.anagrafica where anagrafica.id = soggetti_contrattuali.id_anagrafica and NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "' and COD_TIPOLOGIA_OCCUPANTE <> 'INTE' and id_contratto=" & idContratto
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                tblComponenti = tblComponenti & "<tr><td width='550px' class='style1'><b>COGNOME e NOME </b> " & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</td><td class='style1'><b>COD. FISCALE </b>" & par.IfNull(myReader("COD_FISCALE"), "") & "</td></tr>"
            End While
            myReader.Close()

            If tblComponenti = "<table>" Then
                tblComponenti = tblComponenti & "<tr><td class='style1'>Nessun altro componente.</td></tr>"
            End If

            tblComponenti = tblComponenti & "</table>"

            contenuto = Replace(contenuto, "$compNucleo$", tblComponenti)

            Dim valoreMQ As Decimal = 0
            par.cmd.CommandText = "SELECT siscom_mi.unita_immobiliari.ID," & _
            " unita_immobiliari.cod_unita_immobiliare," & _
            " tipologia_unita_immobiliari.descrizione, unita_immobiliari.interno," & _
            " (scale_edifici.descrizione) AS scala," & _
            " identificativi_catastali.foglio, identificativi_catastali.numero," & _
            " identificativi_catastali.sub," & _
            " tipo_livello_piano.descrizione AS piano," & _
            " identificativi_catastali.SEZIONE" & _
            " FROM siscom_mi.tipo_livello_piano," & _
            " siscom_mi.unita_immobiliari," & _
            " siscom_mi.tipologia_unita_immobiliari," & _
            " siscom_mi.scale_edifici," & _
            " siscom_mi.identificativi_catastali," & _
            " siscom_mi.rapporti_utenza," & _
            " siscom_mi.unita_contrattuale" & _
            " WHERE siscom_mi.tipo_livello_piano.cod(+) = unita_immobiliari.cod_tipo_livello_piano" & _
            " AND unita_immobiliari.id_scala = scale_edifici.ID(+)" & _
            " AND tipologia_unita_immobiliari.cod = unita_immobiliari.cod_tipologia" & _
            " AND unita_immobiliari.id_catastale = identificativi_catastali.ID(+)" & _
            " AND unita_immobiliari.ID = unita_contrattuale.id_unita" & _
            " AND unita_contrattuale.id_contratto = rapporti_utenza.ID" & _
            " AND rapporti_utenza.ID = " & idContratto & "" & _
            " ORDER BY unita_immobiliari.cod_unita_immobiliare ASC"

            Dim tbl1 As String = "<ol>"
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                valoreMQ = 0
                par.cmd.CommandText = "SELECT * from siscom_mi.dimensioni where COD_TIPOLOGIA= 'SUP_NETTA' AND id_unita_immobiliare=" & par.IfNull(myReader("ID"), "")
                Dim myReaderMQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderMQ.Read Then
                    valoreMQ = par.IfEmpty(myReaderMQ("VALORE"), "-----")
                End If
                myReaderMQ.Close()

                tbl1 = tbl1 & "<li><b>Uso</b>: " & par.IfNull(myReader("DESCRIZIONE"), "") & " Supef. calpestabile: <b>mq</b> " & valoreMQ & " Piano " & par.IfNull(myReader("PIANO"), "_______") & " Scala" _
                   & " " & par.IfNull(myReader("scala"), "_______") & " Partita/Sezione " & par.IfNull(myReader("sezione"), "_______") & " Foglio " & par.IfNull(myReader("foglio"), "_______") & " Mappale " & par.IfNull(myReader("NUMERO"), "_______") & " Sub " & par.IfNull(myReader("sub"), "_______") & "</li>"
            End While
            myReader.Close()

            tbl1 = tbl1 & "</ol>"

            contenuto = Replace(contenuto, "$tipiUI$", tbl1)

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F90','Modulo Denuncia Tassa Rifiuti')"
            par.cmd.ExecuteNonQuery()


            Dim PercorsoBarCode As String = par.RicavaBarCode(25, idContratto)
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
            Session.Add("ERRORE", "Provenienza:Modulo Tassa Rifiuti" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
