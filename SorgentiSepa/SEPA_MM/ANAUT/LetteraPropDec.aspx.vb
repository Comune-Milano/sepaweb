Imports System.IO

Partial Class ANAUT_LetteraPropDec
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim NomeFile As String

        Dim COGNOME As String = ""
        Dim NOME As String = ""
        Dim DATA_NASCITA As String = ""
        Dim COMUNE_NASCITA As String = ""
        Dim PROVINCIA_NASCITA As String = ""
        Dim CITTADINANZA As String = ""
        Dim RESIDENZA As String = ""
        Dim INDIRIZZO As String = ""
        Dim TELEFONO As String = ""
        Dim DOCUMENTO As String = ""
        Dim NUMERO_DOCUMENTO As String = ""
        Dim DATA_DOCUMENTO As String = ""
        Dim AUTORITA As String = ""
        Dim UFFICIO = ""

        Dim composizione As String = ""
        Dim datialloggio As String = ""
        Dim requisiti As String = ""
        Dim POSIZIONE As String = ""

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\PropostaDecadenza.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()


            composizione = "</br><table style='width:90%;'><tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>COGNOME</td><td>NOME</td><td>CODICE FISCALE</td></tr>"



            par.cmd.CommandText = "SELECT utenza_comp_nucleo.*,UTENZA_DICHIARAZIONI.POSIZIONE FROM utenza_comp_nucleo,UTENZA_DICHIARAZIONI WHERE UTENZA_DICHIARAZIONI.ID=utenza_comp_nucleo.ID_DICHIARAZIONE AND utenza_comp_nucleo.ID_dichiarazione=" & Request.QueryString("ID") & " order by progr asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read

                composizione = composizione & "<tr style='font-family: ARIAL; font-size: 10pt;'><td>" & par.IfNull(myReader("COGNOME"), "") & "</td><td>" & par.IfNull(myReader("NOME"), "") & "</td><td>" & par.IfNull(myReader("COD_FISCALE"), "") & "</td></tr>"
                POSIZIONE = par.IfNull(myReader("POSIZIONE"), "")
            Loop
            myReader.Close()
            composizione = composizione & "</table>"

            par.cmd.CommandText = "SELECT comuni_nazioni.nome as comunedi,UNITA_IMMOBILIARI.*,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,INDIRIZZI.CAP FROM comuni_nazioni,SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI WHERE indirizzi.cod_comune=comuni_nazioni.cod (+) and  UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+) AND  UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE='" & POSIZIONE & "'"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                datialloggio = "Codice " & par.IfNull(myReader("cod_unita_immobiliare"), "") & " sito in " & par.IfNull(myReader("DESCRIZIONE"), "") & ", " & par.IfNull(myReader("CIVICO"), "") & " CAP " & par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("comunedi"), "")
            End If
            myReader.Close()

            requisiti = "</br><table style='width:90%;'><tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>REQUISITO NON PIU POSSEDUTO</td></tr>"
            par.cmd.CommandText = "SELECT utenza_prop_decadenza.* FROM utenza_prop_decadenza WHERE id_DICHIARAZIONe=" & Request.QueryString("ID") & " order by id desc"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("m1"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Cittadinanza  o Soggiorno;</td></tr>"
                End If
                If par.IfNull(myReader("m2"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Assenza di assegnazione in proprietà;</td></tr>"
                End If
                If par.IfNull(myReader("m3"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Assenza di Decadenza per Attività Illecite;</td></tr>"
                End If
                If par.IfNull(myReader("m4"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Assenza di cessione alloggio ERP;</td></tr>"
                End If
                If par.IfNull(myReader("m5"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Assenza di possesso UI adeguata al nucleo e/o di valore (RR 1/2004 art. 18);</td></tr>"
                End If
                If par.IfNull(myReader("m6"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Assenza di morosità da alloggio ERP ultimi 5 anni;</td></tr>"
                End If
                If par.IfNull(myReader("m7"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>5/2008 (ex art.8 R.R. 1/2007 c.l. lett i) Occupazione abusiva ultimi 5 anni;</td></tr>"
                End If
                If par.IfNull(myReader("m8"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Inutilizzo dell'alloggio;</td></tr>"
                End If
                If par.IfNull(myReader("m9"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Cambio destinazione d'uso;</td></tr>"
                End If
                If par.IfNull(myReader("m10"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Gravi Danni;</td></tr>"
                End If
                If par.IfNull(myReader("m11"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Utilizzo per attività illecite;</td></tr>"
                End If
                If par.IfNull(myReader("m12"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Inadempimento a seguito di diffida;</td></tr>"
                End If
                If par.IfNull(myReader("m13"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Inadempimento art. 20-21 RR 1/2004;</td></tr>"
                End If
                If par.IfNull(myReader("m14"), "0") = "0" Then
                    requisiti = requisiti & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>Valore Immobile superiore al limite;</td></tr>"
                End If
            End If
            requisiti = requisiti & "</table>"
            myReader.Close()








            NomeFile = Format(Now, "yyyyMMddHHmmss")

            'apro e memorizzo il testo base del contratto

            contenuto = Replace(contenuto, "$composizione$", composizione)
            contenuto = Replace(contenuto, "$datialloggio$", datialloggio)
            contenuto = Replace(contenuto, "$requisiti$", requisiti)


            'scrivo il nuovo contratto compilato
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("REPORT\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()


            Response.Redirect("REPORT\" & NomeFile & ".htm")




            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Denuncia Cessione Fabbricato" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
