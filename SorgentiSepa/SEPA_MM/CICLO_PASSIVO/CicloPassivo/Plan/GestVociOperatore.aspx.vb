
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_GestVociOperatore
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
            idoperatore.Value = Request.QueryString("IDO")
            CaricaStato()
        End If
    End Sub

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

            par.cmd.CommandText = "select * FROM OPERATORI WHERE ID=" & idoperatore.Value
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblOperatore.Text = "OPERATORE: " & par.IfNull(myReader5("OPERATORE"), "")
                idxstruttura.Value = par.IfNull(myReader5("id_ufficio"), "-1")
            End If
            myReader5.Close()

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

        par.cmd.CommandText = "select * FROM SISCOM_MI.PF_VOCI_OPERATORI where id_voce in (select id from siscom_mi.pf_voci where id_piano_finanziario=" & idPianoF.Value & ") and ID_OPERATORE=" & idoperatore.Value
        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Do While myReader5.Read
            ListaVoci.Items.FindByValue(myReader5("ID_VOCE")).Selected = True
        Loop
        myReader5.Close()

       

    End Function

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim j As Integer = 0

            For j = 0 To ListaVoci.Items.Count - 1
                If ListaVoci.Items(j).Selected = True Then
                    par.cmd.CommandText = "select * FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & idoperatore.Value & " and ID_VOCE=" & ListaVoci.Items(j).Value
                    Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader5.HasRows = False Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_VOCI_OPERATORI (id_voce,id_operatore) values (" & ListaVoci.Items(j).Value & "," & idoperatore.Value & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader5.Close()
                Else
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_VOCE=" & ListaVoci.Items(j).Value & " AND ID_OPERATORE=" & idoperatore.Value
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            modificato.Value = "0"
            Response.Write("<script>alert('Operazione effettuata correttamente!');</script>")

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
