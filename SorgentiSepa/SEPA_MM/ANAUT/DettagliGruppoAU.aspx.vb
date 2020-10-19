
Partial Class ANAUT_DettagliGruppoAU
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        
        If Not IsPostBack Then
            Response.Flush()
            Carica()
        End If
    End Sub

    Private Function Carica()
        Try
            Dim comunicazioni As String = ""
            Dim LimiteIsee As Integer = 0
            Dim DAFARE As Boolean
            Dim CANONE91 As String = ""
            Dim dt As New System.Data.DataTable
            Dim ROW As System.Data.DataRow
            Dim I As Integer = 0
            Dim NUMERORIGHE As Long = 0
            Dim Contatore As Long = 0
            Dim Anomalia As Boolean = False

            par.OracleConn.Open()
            par.SettaCommand(par)


            dt.Columns.Add("COD_CONTRATTO")
            dt.Columns.Add("PG_AU")
            dt.Columns.Add("COGNOME")
            dt.Columns.Add("NOME")
            dt.Columns.Add("TIPOLOGIA")
            dt.Columns.Add("DECORRENZA")
            dt.Columns.Add("SCADENZA")
            dt.Columns.Add("INDIRIZZO_UNITA")
            dt.Columns.Add("CIVICO_UNITA")
            dt.Columns.Add("COMUNE_UNITA")
            dt.Columns.Add("CAP_UNITA")
            dt.Columns.Add("FILIALE")
            dt.Columns.Add("PREVALENTE")
            dt.Columns.Add("PRESENZA_15")
            dt.Columns.Add("PRESENZA_65")
            dt.Columns.Add("N_INV_100_CON")
            dt.Columns.Add("N_INV_100_SENZA")
            dt.Columns.Add("N_INV_66_99")
            dt.Columns.Add("ID_DICHIARAZIONE")
            dt.Columns.Add("ID_GRUPPO")



            Dim IDAU As Long = 0
            par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                IDAU = myReader("ID")
            End If
            myReader.Close()

            'par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.COGNOME,UTENZA_COMP_NUCLEO.NOME,UTENZA_DICHIARAZIONI.* FROM UTENZA_COMP_NUCLEO,UTENZA_DICHIARAZIONI,UTENZA_GRUPPI_DICHIARAZIONI WHERE UTENZA_COMP_NUCLEO.PROGR=0 AND UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID AND UTENZA_DICHIARAZIONI.ID=UTENZA_GRUPPI_DICHIARAZIONI.ID_DICHIARAZIONE AND UTENZA_GRUPPI_DICHIARAZIONI.APPLICA_AU=0 AND UTENZA_GRUPPI_DICHIARAZIONI.ID_GRUPPO = " & Request.QueryString("id")
            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.ID AS IDAU,UTENZA_DICHIARAZIONI.pg AS PG_AU,UTENZA_DICHIARAZIONI.N_COMP_NUCLEO,UTENZA_DICHIARAZIONI.N_INV_100_CON,UTENZA_DICHIARAZIONI.N_INV_100_SENZA,UTENZA_DICHIARAZIONI.N_INV_100_66 AS n_inv_66_99," _
                               & "DECODE(PREVALENTE_DIP,0,'NO',1,'SI') AS PREVALENTE,DECODE(PRESENZA_MIN_15,0,'NO',1,'SI') AS PRESENZA_15,DECODE(PRESENZA_MAG_65,0,'NO',1,'SI') AS PRESENZA_65, RAPPORTI_UTENZA.ID AS IDC,COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA," _
                               & "TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,TO_CHAR(TO_DATE(DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
                               & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO_UNITA,INDIRIZZI.CIVICO AS CIVICO_UNITA,INDIRIZZI.CAP AS CAP_UNITA,INDIRIZZI.LOCALITA AS COMUNE_UNITA,(TAB_FILIALI.NOME ) AS FILIALE  " _
                               & "FROM siscom_mi.TAB_FILIALI,siscom_mi.COMPLESSI_IMMOBILIARI, siscom_mi.EDIFICI, UTENZA_DICHIARAZIONI,siscom_mi.indirizzi,siscom_mi.rapporti_utenza,siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari,siscom_mi.soggetti_contrattuali,siscom_mi.anagrafica  WHERE " _
                               & "UTENZA_DICHIARAZIONI.ID IN (SELECT ID_DICHIARAZIONE FROM UTENZA_GRUPPI_DICHIARAZIONI WHERE ID_DICHIARAZIONE IS NOT NULL AND ID_GRUPPO=" & Request.QueryString("id") & ") " _
                               & "AND UTENZA_DICHIARAZIONI.RAPPORTO=RAPPORTI_UTENZA.COD_CONTRATTO AND COMPLESSI_IMMOBILIARI.id_filiale=TAB_FILIALI.ID (+) AND " _
                               & "COMPLESSI_IMMOBILIARI.ID=EDIFICI.id_complesso AND EDIFICI.ID=UNITA_IMMOBILIARI.id_edificio  AND  ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                               & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND INDIRIZZI.ID = UNITA_IMMOBILIARI.ID_INDIRIZZO AND " _
                               & "UNITA_IMMOBILIARI.ID = unita_contrattuale.id_unita AND unita_contrattuale.id_contratto = rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL AND  " _
                               & "COD_CONTRATTO IS NOT NULL ORDER BY descrizione ASC,indirizzi.civico ASC,anagrafica.cognome ASC,anagrafica.nome ASC"
            myReader = par.cmd.ExecuteReader()
            While myReader.Read


                DAFARE = True
                

                If DAFARE = True Then

                    I = I + 1


                    ROW = dt.NewRow()
                    ROW.Item("COD_CONTRATTO") = par.IfNull(myReader("COD_CONTRATTO"), "")
                    ROW.Item("PG_AU") = par.IfNull(myReader("PG_AU"), "")
                    ROW.Item("COGNOME") = par.IfNull(myReader("COGNOME"), "")
                    ROW.Item("NOME") = par.IfNull(myReader("NOME"), "")
                    ROW.Item("TIPOLOGIA") = par.IfNull(myReader("TIPOLOGIA"), "")
                    ROW.Item("DECORRENZA") = par.IfNull(myReader("DECORRENZA"), "")
                    ROW.Item("SCADENZA") = par.IfNull(myReader("SCADENZA"), "")
                    ROW.Item("INDIRIZZO_UNITA") = par.IfNull(myReader("INDIRIZZO_UNITA"), "")
                    ROW.Item("CIVICO_UNITA") = par.IfNull(myReader("CIVICO_UNITA"), "")
                    ROW.Item("COMUNE_UNITA") = par.IfNull(myReader("COMUNE_UNITA"), "")
                    ROW.Item("CAP_UNITA") = par.IfNull(myReader("CAP_UNITA"), "")
                    ROW.Item("FILIALE") = par.IfNull(myReader("FILIALE"), "")
                    ROW.Item("PREVALENTE") = par.IfNull(myReader("PREVALENTE"), "")
                    ROW.Item("PRESENZA_15") = par.IfNull(myReader("PRESENZA_15"), "")
                    ROW.Item("PRESENZA_65") = par.IfNull(myReader("PRESENZA_65"), "")
                    ROW.Item("N_INV_100_CON") = par.IfNull(myReader("N_INV_100_CON"), "")
                    ROW.Item("N_INV_100_SENZA") = par.IfNull(myReader("N_INV_100_SENZA"), "")
                    ROW.Item("N_INV_66_99") = par.IfNull(myReader("N_INV_66_99"), "")
                    ROW.Item("ID_DICHIARAZIONE") = myReader("IDAU")
                    ROW.Item("ID_GRUPPO") = Request.QueryString("id")
                    dt.Rows.Add(ROW)
                End If



                
            End While
            myReader.Close()

            If I > 0 Then
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
                Label1.Text = "Elenco AU (" & DataGrid1.Items.Count & " nella lista)"
            Else
                Response.Write("<script>alert('Attenzione, per tutte le AU di questo gruppo è stato già calcolato ed applicato il canone.');</script>")
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        SpostaRegistro()
    End Sub

    Private Function SpostaRegistro()
        Try
            Dim Registro As Long
            Dim IDAU As Long

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                IDAU = myReader("ID")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT ID FROM UTENZA_GRUPPI_LAVORO WHERE FL_REGISTRO=1 AND ID_BANDO_AU=" & IDAU
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Registro = myReader("ID")
            End If
            myReader.Close()


            Dim oDataGridItem As DataGridItem
            Dim chkExport As System.Web.UI.WebControls.CheckBox
            Dim I As Long = 0

            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("ChSelezionato")
                If chkExport.Checked Then

                    par.cmd.CommandText = "DELETE FROM UTENZA_GRUPPI_DICHIARAZIONI  WHERE ID_DICHIARAZIONE=" & oDataGridItem.Cells(19).Text & " AND ID_GRUPPO=" & oDataGridItem.Cells(20).Text
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "DELETE FROM UTENZA_GRUPPI_DICHIARAZIONI  WHERE ID_DICHIARAZIONE=" & oDataGridItem.Cells(19).Text & " AND ID_GRUPPO=" & Registro
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO UTENZA_GRUPPI_DICHIARAZIONI (ID,ID_GRUPPO,ID_DICHIARAZIONE,APPLICA_AU) VALUES (SEQ_UTENZA_GR_DIC.NEXTVAL," & Registro & "," & oDataGridItem.Cells(19).Text & ",0)"
                    par.cmd.ExecuteNonQuery()

                End If
                I = I + 1
            Next

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If I > 0 Then

                Response.Write("<script>alert('Operazione Effettuata! Le dichiarazioni selezionate sono state spostate nel registro.');</script>")
                Carica()
            Else
                Response.Write("<script>alert('Selezionare almeno un elemento dalla lista.');</script>")
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try



    End Function

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        SpostaRegistro()
    End Sub
End Class
