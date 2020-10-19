' TAB ELENCO MANDATI DI PAGAMENTI 
Imports System.Collections

Partial Class Tab_ElencoMandati
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Response.Expires = 0

            If Not IsPostBack Then

                If Request.QueryString("ID_PAGAMENTO") <> "" Then
                    Id_Pagamento = Request.QueryString("ID_PAGAMENTO")
                End If

                BindGrid1()

            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub



    Public Property Id_Pagamento() As Long
        Get
            If Not (ViewState("par_Id_Pagamento") Is Nothing) Then
                Return CLng(ViewState("par_Id_Pagamento"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_Id_Pagamento") = value
        End Set

    End Property




    'PAGAMENTI GRID1
    Private Sub BindGrid1()

        Dim SommaDiff As Decimal = 0
        Dim Sommatot As Decimal = 0
        Dim Sommapagato As Decimal = 0

        Dim FlagConnessione As Boolean


        Try
            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            par.cmd.CommandText = " Select PAGAMENTI_LIQUIDATI.ID ,(PAGAMENTI_LIQUIDATI.NUM_MANDATO||'/'||PAGAMENTI_LIQUIDATI.ANNO_MANDATO) as NUM_MANDATO_ANNO," _
                                     & " to_char(to_date(substr(PAGAMENTI_LIQUIDATI.DATA_MANDATO,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_MANDATO, " _
                                     & " TRIM(TO_CHAR( nvl(PAGAMENTI_LIQUIDATI.IMPORTO,0) ,'9G999G999G999G999G990D99')) as IMPORTO " _
                                 & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                                 & " where PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=" & Id_Pagamento _
                                 & " order by PAGAMENTI_LIQUIDATI.DATA_MANDATO "


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataTable()

            da.Fill(ds)


            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            da.Dispose()
            ds.Dispose()

            'IMPORTO PAGATO
            par.cmd.CommandText = " select sum(nvl(PAGAMENTI_LIQUIDATI.IMPORTO,0) ) as IMPORTO " _
                                           & " from  SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                                           & " where ID_PAGAMENTO=" & Id_Pagamento

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                SommaPagato = par.IfNull(myReader1(0), 0)
            End If
            myReader1.Close()
            lbl_Tot_Liquidato.Text = Format(SommaPagato, "##,##0.00")


            'IMPORTO TOTALE
            par.cmd.CommandText = " select  ( nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0)) as IMPORTO " _
                                           & " from  SISCOM_MI.PAGAMENTI " _
                                           & " where ID=" & Id_Pagamento

            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                SommaTot = par.IfNull(myReader1(0), 0)
            End If
            myReader1.Close()
            lbl_Tot_Da_liquidare.Text = Format(SommaTot, "##,##0.00")


            SommaDiff = Sommatot - Sommapagato
            lbl_Totale.Text = Format(SommaDiff, "##,##0.00")

            '*************************


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Response.Write(ex.Message)
        End Try


    End Sub

    Private Sub imgUscita_Click(sender As Object, e As EventArgs) Handles imgUscita.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "1"
        Response.Write("<script>window.close();</script>")
    End Sub


End Class
