Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_TestoContratti_Ospitalita
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

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


        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\Disdetta.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=67"
            'Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    contenuto = Replace(contenuto, "$ufficio$", par.IfNull(myReaderA("VALORE"), ""))
            'End If
            'myReaderA.Close()

            par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=68"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                contenuto = Replace(contenuto, "$cognomeDirettore$", par.IfNull(myReaderA("VALORE"), ""))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=69"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                contenuto = Replace(contenuto, "$nomeDirettore$", par.IfNull(myReaderA("VALORE"), ""))
            End If
            myReaderA.Close()

            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=70"
            'myReaderA = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    contenuto = Replace(contenuto, "$nascitaDirettore$", par.FormattaData(par.IfNull(myReaderA("VALORE"), "")))
            'End If
            'myReaderA.Close()

            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=71"
            'myReaderA = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    contenuto = Replace(contenuto, "$comunenascitaDirettore$", par.IfNull(myReaderA("VALORE"), ""))
            'End If
            'myReaderA.Close()

            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=72"
            'myReaderA = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    contenuto = Replace(contenuto, "$provincianascitaDirettore$", par.IfNull(myReaderA("VALORE"), ""))
            'End If
            'myReaderA.Close()

            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=73"
            'myReaderA = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    contenuto = Replace(contenuto, "$indirizzoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
            'End If
            'myReaderA.Close()

            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=74"
            'myReaderA = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    contenuto = Replace(contenuto, "$telefonoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
            'End If
            'myReaderA.Close()

            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=76"
            'myReaderA = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    contenuto = Replace(contenuto, "$contratto$", par.IfNull(myReaderA("VALORE"), ""))
            'End If
            'myReaderA.Close()

            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=75"
            'myReaderA = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    contenuto = Replace(contenuto, "$comuneresidenzaDirettore$", par.IfNull(myReaderA("VALORE"), ""))
            'End If
            'myReaderA.Close()

            contenuto = Replace(contenuto, "$dataoggi$", Format(Now, "dd/MM/yyyy"))

            Dim IdContratto As Long = 0
            Dim IdUnita As Long = 0

            par.cmd.CommandText = "SELECT rapporti_utenza.id as ""idcontratto"",data_decorrenza,data_scadenza,DATA_DISDETTA_LOCATARIO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.DATA_STIPULA,RAPPORTI_UTENZA.PRESSO_COR,TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",COMUNI_NAZIONI.SIGLA,COMUNI_NAZIONI.NOME,UNITA_CONTRATTUALE.* FROM SISCOM_MI.TIPO_LIVELLO_PIANO,COMUNI_NAZIONI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA WHERE TIPO_LIVELLO_PIANO.COD=UNITA_CONTRATTUALE.COD_TIPO_LIVELLO_PIANO AND COMUNI_NAZIONI.COD=UNITA_CONTRATTUALE.COD_COMUNE AND RAPPORTI_UTENZA.COD_CONTRATTO='" & Request.QueryString("COD") & "' AND  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND  UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                IdContratto = par.IfNull(myReaderA("idcontratto"), 0)
                IdUnita = par.IfNull(myReaderA("id_unita"), 0)
                contenuto = Replace(contenuto, "$comune$", par.IfNull(myReaderA("NOME"), "__________________"))
                contenuto = Replace(contenuto, "$provincia$", par.IfNull(myReaderA("SIGLA"), "__________________"))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderA("INDIRIZZO"), "__________________"))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReaderA("CIVICO"), "__________________"))
                contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderA("CAP"), ""))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReaderA("PIANO"), "__________________"))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReaderA("SCALA"), "__________________"))
                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReaderA("INTERNO"), "__________________"))
                contenuto = Replace(contenuto, "$vani$", "")
                contenuto = Replace(contenuto, "$accessori$", "")
                contenuto = Replace(contenuto, "$ingressi$", "")
                contenuto = Replace(contenuto, "$codicecontratto$", par.IfNull(myReaderA("COD_CONTRATTO"), "__________________"))
                contenuto = Replace(contenuto, "$datadecorrenza$", par.FormattaData(par.IfNull(myReaderA("data_decorrenza"), "__________________")))
                contenuto = Replace(contenuto, "$datascadenza$", par.FormattaData(par.IfNull(myReaderA("data_scadenza"), "__________________")))

                contenuto = Replace(contenuto, "$codiceunita$", par.IfNull(myReaderA("COD_UNITA_IMMOBILIARE"), "__________________"))
                contenuto = Replace(contenuto, "$pressocor$", par.IfNull(myReaderA("PRESSO_COR"), "__________________"))
                contenuto = Replace(contenuto, "$datastipula$", par.FormattaData(par.IfNull(myReaderA("DATA_STIPULA"), "")))
                contenuto = Replace(contenuto, "$datadisdettalocatario$", par.FormattaData(par.IfNull(myReaderA("DATA_DISDETTA_LOCATARIO"), "__________________")))

            Else
                contenuto = Replace(contenuto, "$comune$", "")
                contenuto = Replace(contenuto, "$provincia$", "")
                contenuto = Replace(contenuto, "$indirizzo$", "")
                contenuto = Replace(contenuto, "$civico$", "")
                contenuto = Replace(contenuto, "$cap$", "")
                contenuto = Replace(contenuto, "$piano$", "")
                contenuto = Replace(contenuto, "$scala$", "")
                contenuto = Replace(contenuto, "$interno$", "")
                contenuto = Replace(contenuto, "$vani$", "")
                contenuto = Replace(contenuto, "$accessori$", "")
                contenuto = Replace(contenuto, "$ingressi$", "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select RAPPORTI_UTENZA.ID,ANAGRAFICA.DATA_DOC,ANAGRAFICA.RILASCIO_DOC,ANAGRAFICA.NUM_DOC,ANAGRAFICA.TIPO_DOC,ANAGRAFICA.TELEFONO,ANAGRAFICA.RESIDENZA,COMUNI_NAZIONI.SIGLA,COMUNI_NAZIONI.NOME AS ""COMUNE_NASCITA"",ANAGRAFICA.DATA_NASCITA,anagrafica.cognome,anagrafica.nome,RAPPORTI_UTENZA.ID_COMMISSARIATO from COMUNI_NAZIONI,SISCOM_MI.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza where ANAGRAFICA.COD_COMUNE_NASCITA=COMUNI_NAZIONI.COD (+) AND rapporti_utenza.cod_contratto='" & Request.QueryString("COD") & "' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                COGNOME = par.IfNull(myReader("COGNOME"), "__________________")
                NOME = par.IfNull(myReader("NOME"), "__________________")
                DATA_NASCITA = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "__________________"))

                If par.IfNull(myReader("SIGLA"), "") <> "E" Then
                    COMUNE_NASCITA = par.IfNull(myReader("COMUNE_NASCITA"), "__________________")
                    PROVINCIA_NASCITA = par.IfNull(myReader("SIGLA"), "__________________")
                    CITTADINANZA = "ITALIANA"
                Else
                    PROVINCIA_NASCITA = par.IfNull(myReader("COMUNE_NASCITA"), "__________________")
                    COMUNE_NASCITA = ""
                    CITTADINANZA = PROVINCIA_NASCITA

                End If
                If InStr(par.IfNull(myReader("RESIDENZA"), ""), "(", CompareMethod.Text) > 0 Then
                    RESIDENZA = Mid(par.IfNull(myReader("RESIDENZA"), ""), 1, InStr(par.IfNull(myReader("RESIDENZA"), ""), "(", CompareMethod.Text) - 1)
                    INDIRIZZO = Mid(par.IfNull(myReader("RESIDENZA"), ""), InStr(par.IfNull(myReader("RESIDENZA"), ""), "CAP", CompareMethod.Text) + 9, Len(par.IfNull(myReader("RESIDENZA"), "")))
                Else
                    RESIDENZA = ""
                    INDIRIZZO = par.IfNull(myReader("RESIDENZA"), "__________________")
                End If
                TELEFONO = par.IfNull(myReader("TELEFONO"), "__________________")

                If par.IfNull(myReader("TIPO_DOC"), "0") = "0" Then
                    DOCUMENTO = "CARTA DI IDENTITA'"
                End If
                If par.IfNull(myReader("TIPO_DOC"), "0") = "1" Then
                    DOCUMENTO = "PASSAPORTO"
                End If

                NUMERO_DOCUMENTO = par.IfNull(myReader("NUM_DOC"), "__________________")
                DATA_DOCUMENTO = par.FormattaData(par.IfNull(myReader("DATA_DOC"), "__________________"))
                AUTORITA = par.IfNull(myReader("RILASCIO_DOC"), "__________________")

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TAB_COMMISSARIATI WHERE ID=" & par.IfNull(myReader("ID_COMMISSARIATO"), -1)
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    contenuto = Replace(contenuto, "$commissariato$", par.IfNull(myReaderA("DESCRIZIONE"), ""))
                Else
                    contenuto = Replace(contenuto, "$commissariato$", "__________________")
                End If
                myReaderA.Close()
                If NUMERO_DOCUMENTO = "" Then
                    Response.Write("<script>alert('Attenzione, mancano i dati relativi al documento di riconoscimento! Aprire l\'anagrafica e verificare!');</script>")
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & myReader("ID") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F25','DATI INCOMPLETI')"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & myReader("ID") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F25','')"
                    par.cmd.ExecuteNonQuery()
                End If



            End If
            myReader.Close()

            par.cmd.CommandText = "select VALORE from SISCOM_MI.DIMENSIONI_unita_contrattuale where ID_CONTRATTO=" & IdContratto & " AND ID_UNITA=" & IdUnita & " AND COD_TIPOLOGIA='SUP_CONV'"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                contenuto = Replace(contenuto, "$superficie$", par.IfNull(myReaderA("VALORE"), 0))
            End If
            myReaderA.Close()


            NomeFile = Format(Now, "yyyyMMddHHmmss")

            contenuto = Replace(contenuto, "$cognome$", COGNOME)
            contenuto = Replace(contenuto, "$nome$", NOME)
            contenuto = Replace(contenuto, "$datanascita$", DATA_NASCITA)
            contenuto = Replace(contenuto, "$comunenascita$", COMUNE_NASCITA)
            contenuto = Replace(contenuto, "$provincianascita$", PROVINCIA_NASCITA)
            contenuto = Replace(contenuto, "$cittadinanza$", CITTADINANZA)
            contenuto = Replace(contenuto, "$residenza$", RESIDENZA)
            contenuto = Replace(contenuto, "$indirizzo$", INDIRIZZO)
            contenuto = Replace(contenuto, "$telefono$", TELEFONO)
            contenuto = Replace(contenuto, "$documento$", DOCUMENTO)
            contenuto = Replace(contenuto, "$numerodocumento$", NUMERO_DOCUMENTO)
            contenuto = Replace(contenuto, "$datadocumento$", DATA_DOCUMENTO)
            contenuto = Replace(contenuto, "$autorita$", AUTORITA)


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & IdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F90','Lettera di disdetta')"
            par.cmd.ExecuteNonQuery()


            Dim PercorsoBarCode As String = par.RicavaBarCode(22, IdContratto)
            contenuto = Replace(contenuto, "$barcode$", "..\..\..\FileTemp\" & PercorsoBarCode)



            '********** 29/01/2013 NOME FILIALE ************
            par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale where unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & IdContratto & " and indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.id=unita_contrattuale.id_unita and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale"
            Dim myReaderF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReaderF.Read Then
                contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderF("NOME"), ""))
            Else
                par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_contrattuale,siscom_mi.filiali_virtuali where filiali_virtuali.id_contratto=unita_contrattuale.id_contratto and unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & IdContratto & " and indirizzi.id=tab_filiali.id_indirizzo and tab_filiali.id=filiali_virtuali.id_filiale"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader2("NOME"), ""))
                End If
                myReader2.Close()
            End If
            myReaderF.Close()


            'contenuto = Replace(contenuto, "$cognome$", COGNOME)
            'contenuto = Replace(contenuto, "$nome$", NOME)
            'contenuto = Replace(contenuto, "$datanascita$", DATA_NASCITA)
            'contenuto = Replace(contenuto, "$comunenascita$", COMUNE_NASCITA)
            'contenuto = Replace(contenuto, "$provincianascita$", PROVINCIA_NASCITA)
            'contenuto = Replace(contenuto, "$cittadinanza$", CITTADINANZA)
            'contenuto = Replace(contenuto, "$residenza$", RESIDENZA)
            'contenuto = Replace(contenuto, "$indirizzo$", INDIRIZZO)
            'contenuto = Replace(contenuto, "$telefono$", TELEFONO)
            'contenuto = Replace(contenuto, "$documento$", DOCUMENTO)
            'contenuto = Replace(contenuto, "$numerodocumento$", NUMERO_DOCUMENTO)
            'contenuto = Replace(contenuto, "$datadocumento$", DATA_DOCUMENTO)
            'contenuto = Replace(contenuto, "$autorita$", AUTORITA)


            'scrivo il nuovo contratto compilato
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeLettere\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()

            Response.Redirect("..\..\ALLEGATI\CONTRATTI\StampeLettere\" & NomeFile & ".htm")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Lettera Disdetta" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
