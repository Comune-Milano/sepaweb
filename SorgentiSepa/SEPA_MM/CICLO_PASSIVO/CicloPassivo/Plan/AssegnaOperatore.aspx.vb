
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_AssegnaOperatore
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim ds As New Data.DataSet()
    Dim dlist As CheckBoxList
    Dim da As Oracle.DataAccess.Client.OracleDataAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            idPianoF.Value = Request.QueryString("IDP")
            CaricaStato()
        End If
    End Sub

    Function CaricaOperatori()
        ListaOperatori.Items.Clear()
        Dim s As String = "SELECT ID,COGNOME||' '||NOME AS DESCRIZIONE FROM OPERATORI WHERE MOD_CICLO_P=1 AND (BP_COMPILAZIONE=1 or bp_compilazione_l=1) AND id_ufficio<>0 AND cognome IS NOT NULL AND  ID_CAF=2 AND fl_eliminato='0' AND ID NOT IN (SELECT pf_voci_operatori.id_operatore FROM siscom_mi.pf_voci_operatori,siscom_mi.pf_voci WHERE pf_voci_operatori.id_voce=pf_voci.ID AND pf_voci.id_piano_finanziario=" & idPianoF.Value & ") ORDER BY cognome ASC,nome ASC"
        dlist = ListaOperatori

        da = New Oracle.DataAccess.Client.OracleDataAdapter(s, par.OracleConn)
        da.Fill(ds)

        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = "DESCRIZIONE"
        dlist.DataValueField = "ID"

        dlist.DataBind()

        da.Dispose()
        da = Nothing

        dlist.DataSource = Nothing
        dlist = Nothing
    End Function

    Function CaricaVoci()
        ds.Clear()
        ds.Dispose()
        ListaVoci.Items.Clear()
        Dim s As String = "select SISCOM_MI.PF_VOCI.id,SISCOM_MI.PF_VOCI.codice||'-'||SISCOM_MI.PF_VOCI.descrizione as DESCRIZIONE from SISCOM_MI.PF_VOCI where id_voce_madre is null and id_piano_finanziario=" & idPianoF.Value & " ORDER BY codice ASC"
        dlist = ListaVoci

        da = New Oracle.DataAccess.Client.OracleDataAdapter(s, par.OracleConn)
        da.Fill(ds)

        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = "DESCRIZIONE"
        dlist.DataValueField = "ID"

        dlist.DataBind()

        da.Dispose()
        da = Nothing

        dlist.DataSource = Nothing
        dlist = Nothing
    End Function

    '


    Function CaricaStato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")

                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
            End If
            myReader5.Close()

            CaricaOperatori()
            CaricaVoci()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim i As Integer = 0
            Dim j As Integer = 0

            Dim idxOperatore As Long = 0
            Dim idxStruttura As String = "-1"

            For i = 0 To ListaOperatori.Items.Count - 1
                If ListaOperatori.Items(i).Selected = True Then
                    idxOperatore = ListaOperatori.Items(i).Value

                    par.cmd.CommandText = "select * FROM operatori WHERE ID=" & idxOperatore
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        idxStruttura = par.IfNull(myReader("id_ufficio"), "-1")
                    End If
                    myReader.Close()

                    For j = 0 To ListaVoci.Items.Count - 1
                        If ListaVoci.Items(j).Selected = True Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_VOCI_OPERATORI (id_voce,id_operatore) values (" & ListaVoci.Items(j).Value & "," & idxOperatore & ")"
                            par.cmd.ExecuteNonQuery()



                            par.cmd.CommandText = "select * FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE=" & ListaVoci.Items(j).Value & " and ID_STRUTTURA=" & idxStruttura
                            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader5.HasRows = False Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_VOCI_STRUTTURA (ID_VOCE, ID_STRUTTURA, VALORE_NETTO, COMPLETO, IVA, ASSESTAMENTO_VALORE_LORDO, DATA_ASSESTAMENTO, VALORE_LORDO, COMPLETO_ALER, VALORE_LORDO_ALER, COMPLETO_COMUNE) VALUES  (" & ListaVoci.Items(j).Value & ", " & idxStruttura & ", NULL, '0', 0, 0, NULL, 0, '0', 0,  0)"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "select * FROM siscom_mi.pf_voci WHERE ID_voce_madre=" & ListaVoci.Items(j).Value
                                Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                Do While myReader123.Read

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_VOCI_STRUTTURA (ID_VOCE, ID_STRUTTURA, VALORE_NETTO, COMPLETO, IVA, ASSESTAMENTO_VALORE_LORDO, DATA_ASSESTAMENTO, VALORE_LORDO, COMPLETO_ALER, VALORE_LORDO_ALER, COMPLETO_COMUNE) VALUES  (" & myReader123("id") & ", " & idxStruttura & ", NULL, '0', 0, 0, NULL, 0, '0', 0,  0)"
                                    par.cmd.ExecuteNonQuery()

                                Loop
                                myReader123.Close()

                            End If
                            myReader5.Close()

                        End If

                    Next
                End If
            Next


            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            modificato.Value = "0"
            Response.Write("<script>alert('Operazione Effettuata!');</script>")
            CaricaStato()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

        modificato.Value = "0"
    End Sub
End Class
