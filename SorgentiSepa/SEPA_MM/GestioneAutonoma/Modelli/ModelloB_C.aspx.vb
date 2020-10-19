Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class GestioneAutonoma_Modelli_ModelloB_C
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim NomeFile As String
        Dim Filiale As String = ""
        Dim Decorrenza As String = ""
        Dim GruppoAutogest As String = ""
        Dim N_Inquilini As String = ""
        Dim n_firmatari As Double = 0
        Dim perc_firmat As String = ""
        Dim idEdificio As String = ""
        Dim idComplesso As String = ""

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            NomeFile = "Id_" & Request.QueryString("IdGestAutonoma") & "_" & Format(Now, "yyyyMMddHHmmss")



            '***************************SELEZIONE DEL FILE BLOB SE ESISTE SUL DATABASE**********************************
            par.cmd.CommandText = "SELECT TESTO FROM SISCOM_MI.AUTOGESTIONI_MODELLI WHERE ID_AUTOGESTIONE = " & Request.QueryString("IdGestAutonoma") & " AND ID_PROVVEDIMENTO = " & Request.QueryString("IdProv") & " AND TIPO_MODELLO = '" & Request.QueryString("TIPO") & "'"
            Dim bw As BinaryWriter
            Dim lettoreblob As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettoreblob.Read Then
                Dim fileName As String = Server.MapPath("..\Stampe\" & NomeFile & ".htm")
                Dim fs = New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write)
                bw = New BinaryWriter(fs)
                bw.Write(lettoreblob("TESTO"))
                bw.Flush()
                bw.Close()
                Dim srl As StreamReader = New StreamReader(Server.MapPath("..\Stampe\" & NomeFile & ".htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenutol As String = srl.ReadToEnd()
                srl.Close()

                'scrivo LA NUOVA PROPOSTA DI AUTOGESTIONE
                Dim srlet As StreamWriter = New StreamWriter(Server.MapPath("..\Stampe\" & NomeFile & ".htm"), False, System.Text.Encoding.Default)
                srlet.WriteLine(contenutol)
                srlet.Close()
                Response.Redirect("..\EDITOR_HTML\Editor.aspx?F=" & par.Cripta(NomeFile & ".htm") & "&ID_AUTO=" & Request.QueryString("IdGestAutonoma") & "&IDPROV=" & Request.QueryString("IdProv") & "&TIPO=BC")
                Exit Sub


            End If


            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ModB_C.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            par.cmd.CommandText = "SELECT AUTOGESTIONI_PROV.*, AUTOGESTIONI.ID_EDIFICIO,AUTOGESTIONI.ID_COMPLESSO FROM SISCOM_MI.AUTOGESTIONI_PROV,SISCOM_MI.AUTOGESTIONI WHERE AUTOGESTIONI.ID=AUTOGESTIONI_PROV.ID_AUTOGESTIONE AND AUTOGESTIONI_PROV.ID = '" & Request.QueryString("IdProv") & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("ID_COMPLESSO"), 0) <> 0 Then
                    idEdificio = ""
                    idComplesso = myReader("ID_COMPLESSO")
                Else
                    idComplesso = ""
                    idEdificio = myReader("ID_EDIFICIO")
                End If
                contenuto = Replace(contenuto, "$provvedimento$", par.IfNull(myReader("NUM_PROV"), ""))
                contenuto = Replace(contenuto, "$data_provvedimento$", par.FormattaData(par.IfNull(myReader("DATA_PROV"), "")))
                contenuto = Replace(contenuto, "$data_decorrenza$", par.FormattaData(par.IfNull(myReader("DATA_DEC"), "")))
                contenuto = Replace(contenuto, "$inquilini$", par.IfNull(myReader("NUM_AFFITTUARI"), ""))
                contenuto = Replace(contenuto, "$firmatari$", par.IfNull(myReader("NUM_FIRMATARI"), ""))
                contenuto = Replace(contenuto, "$perc_firmatari$", par.IfNull(myReader("PERC_FIRMATARI"), ""))

                contenuto = Replace(contenuto, "$num_alloggi$", par.IfNull(myReader("NUM_ALLOGGI"), ""))
                contenuto = Replace(contenuto, "$num_negozi$", par.IfNull(myReader("NUM_NEGOZI"), ""))
                contenuto = Replace(contenuto, "$num_diversi$", par.IfNull(myReader("NUM_DIVERSI"), ""))

                contenuto = Replace(contenuto, "$sup_alloggi$", par.IfNull(myReader("SUP_ALLOGGI"), ""))
                contenuto = Replace(contenuto, "$sup_negozi$", par.IfNull(myReader("SUP_NEGOZI"), ""))
                contenuto = Replace(contenuto, "$sup_diversi$", par.IfNull(myReader("SUP_DIVERSI"), ""))

                contenuto = Replace(contenuto, "$data_morosita$", par.FormattaData(par.IfNull(myReader("DATA_RIF_FINANZIARIO"), "")))
                contenuto = Replace(contenuto, "$morosita$", par.IfNull(myReader("MOROSITA"), ""))
                contenuto = Replace(contenuto, "$competenze$", par.IfNull(myReader("COMPETENZE"), ""))
                contenuto = Replace(contenuto, "$affitto$", par.IfNull(myReader("AFFITTO"), ""))
                contenuto = Replace(contenuto, "$spese$", par.IfNull(myReader("SPESE"), ""))

                contenuto = Replace(contenuto, "$num_senza_titolo$", par.IfNull(myReader("NUM_SENZA_TITOLO"), ""))
                contenuto = Replace(contenuto, "$num_abusivi$", par.IfNull(myReader("NUM_OA"), ""))
                contenuto = Replace(contenuto, "$cent_termica$", "")


                If par.IfNull(myReader("FL_PROPIETARI"), 0) > 0 Then
                    contenuto = Replace(contenuto, "$proprietari$", "SI")
                Else
                    contenuto = Replace(contenuto, "$proprietari$", "NO")
                End If
                myReader.Close()
            End If

            par.cmd.CommandText = "SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.AUTOGESTIONI WHERE EDIFICI.ID= AUTOGESTIONI.ID_EDIFICIO  AND COMPLESSI_IMMOBILIARI.ID = EDIFICI.ID_COMPLESSO AND TAB_FILIALI.ID = COMPLESSI_IMMOBILIARI.ID_FILIALE AND AUTOGESTIONI.ID =" & Request.QueryString("IdGestAutonoma")
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$filiale$", (par.IfNull(myReader(0), "N.D")))
            Else
                contenuto = Replace(contenuto, "$filiale$", "N.D")
            End If
            myReader.Close()



            par.cmd.CommandText = "SELECT AUTOGESTIONI_TAB_SERVIZI.DESCRIZIONE FROM SISCOM_MI.AUTOGESTIONI_SERVIZI, SISCOM_MI.AUTOGESTIONI_TAB_SERVIZI WHERE AUTOGESTIONI_SERVIZI.ID_SERVIZIO = AUTOGESTIONI_TAB_SERVIZI.ID AND ID_MOD_B =" & Request.QueryString("IdProv")
            myReader = par.cmd.ExecuteReader()
            Dim GRUPPI As String = ""
            Do While myReader.Read
                GRUPPI = GRUPPI & "<p class=BodyText2>- " & (par.IfNull(myReader("DESCRIZIONE"), "")) & ";</p>"
            Loop
            contenuto = Replace(contenuto, "$gruppo_autogestioni$", GRUPPI)
            myReader.Close()




            If Not String.IsNullOrEmpty(idEdificio) Then
                par.cmd.CommandText = "SELECT EDIFICI.COD_EDIFICIO AS COD_IMMOBILE, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO, INDIRIZZI.LOCALITA,INDIRIZZI.CAP,'' AS NUM_SCALE,''AS NUM_PIANI_FUORI FROM SISCOM_MI.INDIRIZZI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND EDIFICI.ID = " & idEdificio
            Else
                par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.COD_COMPLESSO AS COD_IMMOBILE, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO, INDIRIZZI.LOCALITA,INDIRIZZI.CAP,'' AS NUM_SCALE,''AS NUM_PIANI_FUORI FROM SISCOM_MI.INDIRIZZI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID AND COMPLESSI_IMMOBILIARI.ID = " & idComplesso
            End If

            myReader = par.cmd.ExecuteReader()

            If myReader.Read Then
                contenuto = Replace(contenuto, "$indirizzo$", (par.IfNull(myReader("DESCRIZIONE"), "")))
                contenuto = Replace(contenuto, "$civico$", (par.IfNull(myReader("CIVICO"), "")))
                contenuto = Replace(contenuto, "$comune$", (par.IfNull(myReader("LOCALITA"), "")))
                contenuto = Replace(contenuto, "$cap$", (par.IfNull(myReader("CAP"), "")))
                contenuto = Replace(contenuto, "$cod_immobile$", (par.IfNull(myReader("COD_IMMOBILE"), "")))
                contenuto = Replace(contenuto, "$scale$", (par.IfNull(myReader("NUM_SCALE"), "0")))
                contenuto = Replace(contenuto, "$num_piani$", (par.IfNull(myReader("NUM_PIANI_FUORI"), "0")))
                If par.IfNull(myReader("NUM_PIANI_FUORI"), 0) > 0 Then
                    contenuto = Replace(contenuto, "$ascensore$", "SI")
                Else
                    contenuto = Replace(contenuto, "$ascensore$", "NO")
                End If
            End If

            If Not String.IsNullOrEmpty(idEdificio) Then
                par.cmd.CommandText = "SELECT SUM(VALORE) AS MQ_EDIFICIO FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE  EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND EDIFICI.ID =" & idEdificio
            Else
                par.cmd.CommandText = "SELECT SUM(VALORE) AS MQ_EDIFICIO FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND EDIFICI.ID_COMPLESSO = " & idComplesso
            End If
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$mq_imm$", Format(par.IfNull(myReader("MQ_EDIFICIO"), 0), "##,##0.00"))
            End If
            myReader.Close()

            par.OracleConn.Close()






            'scrivo LA NUOVA PROPOSTA DI AUTOGESTIONE
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\Stampe\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()

            Response.Redirect("..\EDITOR_HTML\Editor.aspx?F=" & par.Cripta(NomeFile & ".htm") & "&ID_AUTO=" & Request.QueryString("IdGestAutonoma") & "&IDPROV=" & Request.QueryString("IdProv") & "&TIPO=BC")
            par.OracleConn.Close()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Gestione Autonoma ModB" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub
    Private Sub ApriFile()

    End Sub
End Class
