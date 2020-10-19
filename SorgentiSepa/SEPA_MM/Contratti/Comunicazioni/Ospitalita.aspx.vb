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

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\Ospitalita.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=67"
            'Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    contenuto = Replace(contenuto, "$ufficio$", par.IfNull(myReaderA("VALORE"), ""))
            'End If
            'myReaderA.Close()

            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=68"
            'myReaderA = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    contenuto = Replace(contenuto, "$cognomeDirettore$", par.IfNull(myReaderA("VALORE"), ""))
            'End If
            'myReaderA.Close()

            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=69"
            'myReaderA = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    contenuto = Replace(contenuto, "$nomeDirettore$", par.IfNull(myReaderA("VALORE"), ""))
            'End If
            'myReaderA.Close()

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

            Dim TIPOLOGIA As String = ""
            Dim ID_UNITA As String = ""
            Dim MODERATO As String = ""

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,UNITA_CONTRATTUALE.ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.RAPPORTI_UTENZA WHERE  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND COD_CONTRATTO='" & Request.QueryString("cod") & "'"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                TIPOLOGIA = par.IfNull(myReaderA("COD_TIPOLOGIA_CONTR_LOC"), "")
                ID_UNITA = par.IfNull(myReaderA("ID_UNITA"), "-1")
            End If
            myReaderA.Close()

            If ID_UNITA = "" Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("Non è possibile stampare questo modulo.")
                Exit Sub
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_UI WHERE ID=" & ID_UNITA & " ORDER BY DATA DESC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                MODERATO = "1"
            End If
            myReaderA.Close()

            If Mid(TIPOLOGIA, 1, 3) = "ERP" And MODERATO = "1" Then
                TIPOLOGIA = "L431/98"
            End If

            Select Case Mid(TIPOLOGIA, 1, 3)
                Case "L43"
                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=85"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$ufficio$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=86"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$cognomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=87"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=88"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nascita$", par.FormattaData(par.IfNull(myReaderA("VALORE"), "")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=89"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comunecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=90"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$provinciacedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=91"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$indirizzoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=92"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$telefonoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=93"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comuneresidenzaDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=94"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$contratto$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()
                Case "EQC"
                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=105"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$ufficio$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=106"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$cognomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=107"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=108"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nascita$", par.FormattaData(par.IfNull(myReaderA("VALORE"), "")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=109"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comunecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=110"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$provinciacedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=111"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$indirizzoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=112"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$telefonoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=113"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comuneresidenzaDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=114"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$contratto$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                Case "USD"
                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=95"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$ufficio$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=96"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$cognomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=97"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=98"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nascita$", par.FormattaData(par.IfNull(myReaderA("VALORE"), "")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=99"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comunecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=100"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$provinciacedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=101"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$indirizzoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=102"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$telefonoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=103"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comuneresidenzaDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=104"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$contratto$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()


                Case Else
                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=67"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$ufficio$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=68"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$cognomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=69"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=70"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nascita$", par.FormattaData(par.IfNull(myReaderA("VALORE"), "")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=71"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comunecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=72"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$provinciacedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=73"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$indirizzoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=74"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$telefonoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=76"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$contratto$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=75"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comuneresidenzaDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()
            End Select

            Dim IdContratto As Long = 0

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.dest_USO,rapporti_utenza.id as ""idcontratto"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",COMUNI_NAZIONI.SIGLA,COMUNI_NAZIONI.NOME,UNITA_CONTRATTUALE.* FROM SISCOM_MI.TIPO_LIVELLO_PIANO,COMUNI_NAZIONI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA WHERE  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND TIPO_LIVELLO_PIANO.COD=UNITA_CONTRATTUALE.COD_TIPO_LIVELLO_PIANO AND COMUNI_NAZIONI.COD=UNITA_CONTRATTUALE.COD_COMUNE AND RAPPORTI_UTENZA.COD_CONTRATTO='" & Request.QueryString("COD") & "' AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                IdContratto = par.IfNull(myReaderA("idcontratto"), 0)
                contenuto = Replace(contenuto, "$comune$", par.IfNull(myReaderA("NOME"), ""))
                contenuto = Replace(contenuto, "$provincia$", par.IfNull(myReaderA("SIGLA"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderA("INDIRIZZO"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReaderA("CIVICO"), ""))
                contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderA("CAP"), ""))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReaderA("PIANO"), ""))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReaderA("SCALA"), ""))
                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReaderA("INTERNO"), ""))
                contenuto = Replace(contenuto, "$vani$", "")
                contenuto = Replace(contenuto, "$accessori$", "")
                contenuto = Replace(contenuto, "$ingressi$", "")

                Select Case par.IfNull(myReaderA("dest_USO"), "0")
                    Case "R"
                        contenuto = Replace(contenuto, "$usoimmobile$", "RESIDENZIALE")
                    Case "N"
                        contenuto = Replace(contenuto, "$usoimmobile$", "NEGOZIO COMMERCIALE")
                    Case "B"
                        contenuto = Replace(contenuto, "$usoimmobile$", "POSTO AUTO")
                    Case "L"
                        contenuto = Replace(contenuto, "$usoimmobile$", "LABORATORIO")
                    Case "C"
                        contenuto = Replace(contenuto, "$usoimmobile$", "RESIDENZIALE-COOPERATIVE")
                    Case "0"
                        contenuto = Replace(contenuto, "$usoimmobile$", "________________")
                    Case Else
                        contenuto = Replace(contenuto, "$usoimmobile$", "________________")
                End Select
            Else
                contenuto = Replace(contenuto, "$comune$", "________________")
                contenuto = Replace(contenuto, "$provincia$", "________________")
                contenuto = Replace(contenuto, "$indirizzo$", "________________")
                contenuto = Replace(contenuto, "$civico$", "________________")
                contenuto = Replace(contenuto, "$cap$", "________________")
                contenuto = Replace(contenuto, "$piano$", "________________")
                contenuto = Replace(contenuto, "$scala$", "________________")
                contenuto = Replace(contenuto, "$interno$", "________________")
                contenuto = Replace(contenuto, "$vani$", "________________")
                contenuto = Replace(contenuto, "$accessori$", "________________")
                contenuto = Replace(contenuto, "$ingressi$", "________________")
                contenuto = Replace(contenuto, "$usoimmobile$", "________________")
            End If
            myReaderA.Close()

            If IdContratto <> 0 Then
                Dim s As String = ""

                s = "<table style='width:100%;'><tr><td style='font-family: Garamond; font-size: 12pt'>Nominativo</td><td style='font-family: Garamond; font-size: 12pt'>Cod. Fiscale</td><td style='font-family: Garamond; font-size: 12pt'>Data Ingresso</td><td style='font-family: Garamond; font-size: 12pt'>Data Uscita</td></tr>"

                par.cmd.CommandText = "SELECT * FROM siscom_mi.ospiti WHERE ID_contratto=" & IdContratto & " order by nominativo asc"
                Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderD.Read Then
                    s = s & "<tr><td style='font-family: Garamond; font-size: 10pt'>" & par.IfNull(myReaderD("nominativo"), "") & "</td><td style='font-family: Garamond; font-size: 10pt'>" & par.IfNull(myReaderD("cod_fiscale"), "") & "</td><td style='font-family: Garamond; font-size: 10pt'>" & par.FormattaData(par.IfNull(myReaderD("data_inizio_ospite"), "")) & "</td><td style='font-family: Garamond; font-size: 10pt'>" & par.FormattaData(par.IfNull(myReaderD("data_fine_ospite"), "")) & "</td></tr>"
                End If
                myReaderD.Close()

                s = s & "</table>"
                contenuto = Replace(contenuto, "$ospiti$", s)
            Else
                contenuto = Replace(contenuto, "$ospiti$", "")
            End If


            par.cmd.CommandText = "select ANAGRAFICA.CITTADINANZA,RAPPORTI_UTENZA.ID,ANAGRAFICA.DATA_DOC,ANAGRAFICA.RILASCIO_DOC,ANAGRAFICA.NUM_DOC,ANAGRAFICA.TIPO_DOC,ANAGRAFICA.TELEFONO,ANAGRAFICA.RESIDENZA,COMUNI_NAZIONI.SIGLA,COMUNI_NAZIONI.NOME AS ""COMUNE_NASCITA"",ANAGRAFICA.DATA_NASCITA,anagrafica.cognome,anagrafica.nome,RAPPORTI_UTENZA.ID_COMMISSARIATO from COMUNI_NAZIONI,SISCOM_MI.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza where ANAGRAFICA.COD_COMUNE_NASCITA=COMUNI_NAZIONI.COD (+) AND rapporti_utenza.cod_contratto='" & Request.QueryString("COD") & "' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                COGNOME = par.IfNull(myReader("COGNOME"), "")
                NOME = par.IfNull(myReader("NOME"), "")
                DATA_NASCITA = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), ""))

                If par.IfNull(myReader("SIGLA"), "") <> "E" Then
                    COMUNE_NASCITA = par.IfNull(myReader("COMUNE_NASCITA"), "")
                    PROVINCIA_NASCITA = par.IfNull(myReader("SIGLA"), "")
                    CITTADINANZA = par.IfNull(myReader("CITTADINANZA"), "")
                    If PROVINCIA_NASCITA = "" Then
                        PROVINCIA_NASCITA = CITTADINANZA
                    End If
                Else
                    PROVINCIA_NASCITA = par.IfNull(myReader("COMUNE_NASCITA"), "")
                    COMUNE_NASCITA = ""
                    CITTADINANZA = PROVINCIA_NASCITA

                End If

                'RESIDENZA = Mid(par.IfNull(myReader("RESIDENZA"), ""), 1, InStr(par.IfNull(myReader("RESIDENZA"), ""), "(", CompareMethod.Text) - 1)
                'INDIRIZZO = Mid(par.IfNull(myReader("RESIDENZA"), ""), InStr(par.IfNull(myReader("RESIDENZA"), ""), "CAP", CompareMethod.Text) + 9, Len(par.IfNull(myReader("RESIDENZA"), "")))

                If InStr(par.IfNull(myReader("RESIDENZA"), ""), "(", CompareMethod.Text) > 0 Then
                    RESIDENZA = Mid(par.IfNull(myReader("RESIDENZA"), ""), 1, InStr(par.IfNull(myReader("RESIDENZA"), ""), "(", CompareMethod.Text) - 1)
                    INDIRIZZO = Mid(par.IfNull(myReader("RESIDENZA"), ""), InStr(par.IfNull(myReader("RESIDENZA"), ""), "CAP", CompareMethod.Text) + 9, Len(par.IfNull(myReader("RESIDENZA"), "")))
                Else
                    RESIDENZA = ""
                    INDIRIZZO = par.IfNull(myReader("RESIDENZA"), "")
                End If

                TELEFONO = par.IfNull(myReader("TELEFONO"), "")
                If par.IfNull(myReader("TIPO_DOC"), "0") = "0" Then
                    DOCUMENTO = "CARTA DI IDENTITA'"
                End If
                If par.IfNull(myReader("TIPO_DOC"), "0") = "1" Then
                    DOCUMENTO = "PASSAPORTO"
                End If

                NUMERO_DOCUMENTO = par.IfNull(myReader("NUM_DOC"), "")
                DATA_DOCUMENTO = par.FormattaData(par.IfNull(myReader("DATA_DOC"), ""))
                AUTORITA = par.IfNull(myReader("RILASCIO_DOC"), "")

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TAB_COMMISSARIATI WHERE ID=" & par.IfNull(myReader("ID_COMMISSARIATO"), -1)
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    contenuto = Replace(contenuto, "$commissariato$", par.IfNull(myReaderA("DESCRIZIONE"), ""))
                Else
                    contenuto = Replace(contenuto, "$commissariato$", "")
                End If
                myReaderA.Close()
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & myReader("ID") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F27','')"
                par.cmd.ExecuteNonQuery()

            End If
            myReader.Close()




            NomeFile = Format(Now, "yyyyMMddHHmmss")

            'apro e memorizzo il testo base del contratto



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


            'scrivo il nuovo contratto compilato
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeLettere\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()

            'Dim url As String = NomeFile
            'Dim pdfConverter As PdfConverter = New PdfConverter
            ' ''pdfConverter.LicenseKey = "P38cBx6AWW7b9c81TjEGxnrazP+J7rOjs+9omJ3TUycauK+cL WdrITM5T59hdW5r"
            'pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            'pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            'pdfConverter.PdfDocumentOptions.ShowHeader = False
            'pdfConverter.PdfDocumentOptions.ShowFooter = False
            'pdfConverter.PdfDocumentOptions.LeftMargin = 5
            'pdfConverter.PdfDocumentOptions.RightMargin = 5
            'pdfConverter.PdfDocumentOptions.TopMargin = 5
            'pdfConverter.PdfDocumentOptions.BottomMargin = 5
            'pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

            'pdfConverter.PdfDocumentOptions.ShowHeader = False
            'pdfConverter.PdfFooterOptions.FooterText = ("")
            'pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            'pdfConverter.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter.PdfFooterOptions.PageNumberText = ""
            'pdfConverter.PdfFooterOptions.ShowPageNumber = False


            'pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\StampeLettere\") & NomeFile & ".htm", Server.MapPath("..\StampeLettere\") & NomeFile & ".pdf")

            'System.IO.File.Delete(Server.MapPath("..\StampeLettere\") & NomeFile & ".htm")
            'Response.Redirect("..\StampeLettere\" & NomeFile & ".pdf")
            '            Response.Write("<script>window.open('../StampeLettere/" & NomeFile & ".pdf" & "','Lettera','');</script>")


            Response.Redirect("..\..\ALLEGATI\CONTRATTI\StampeLettere\" & NomeFile & ".htm")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Lettera Denuncia di ospitalità" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
