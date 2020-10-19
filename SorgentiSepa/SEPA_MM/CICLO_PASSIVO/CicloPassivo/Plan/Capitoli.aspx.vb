
Partial Class Contabilita_CicloPassivo_Capitoli
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public ds As New Data.DataSet()
    Public miadt As New Data.DataSet()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            
            idPianoF.Value = Request.QueryString("IDP")

            CaricaValori()



        End If


    End Sub

    Function CaricaValori()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter("select id ,cod from siscom_mi.pf_capitoli ORDER BY cod ASC", par.OracleConn)
            da1.Fill(miadt, "pf_capitoli")



            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
            End If
            myReader5.Close()


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select * from siscom_mi.pf_voci where length(codice)=7 and id_piano_finanziario=" & idPianoF.Value & "  ORDER BY codice ASC,indice asc", par.OracleConn)

            Dim ds1 As New Data.DataSet()
            da.Fill(ds1, "pf_voci")
            DataGridVoci.DataSource = ds1
            DataGridVoci.DataBind()


            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("ID_OPERATORE")
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                If par.IfNull(myReader5("BP_CAPITOLI_L"), "1") = "1" Then
                    ImgProcedi.Visible = False
                    lettura.Value = "1"

                    If lettura.Value = "1" Then
                        Dim i As Integer = 0
                        Dim di As DataGridItem
                        For i = 0 To Me.DataGridVoci.Items.Count - 1
                            di = Me.DataGridVoci.Items(i)
                            CType(di.Cells(3).FindControl("cmbCapitolo"), DropDownList).Enabled = False

                        Next
                    End If

                End If
            End If
            myReader5.Close()


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
            Dim di As DataGridItem
            Dim IdIntestatario As String = ""
            Dim IdUi As String = ""
            Dim Importo As String = ""

            For i = 0 To Me.DataGridVoci.Items.Count - 1
                di = Me.DataGridVoci.Items(i)
                par.cmd.CommandText = "update siscom_mi.pf_voci set id_capitolo=" & par.IfEmpty(DirectCast(di.Cells(1).FindControl("cmbCapitolo"), DropDownList).SelectedItem.Value, "NULL") & " where id=" & Me.DataGridVoci.Items(i).Cells(0).Text
                par.cmd.ExecuteNonQuery()

            Next

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            modificato.Value = "0"
            Response.Write("<script>alert('Operazione Effettuata!');</script>")

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

        modificato.Value = "0"
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender


    End Sub
End Class
