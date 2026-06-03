using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasArancelarias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoArancelio = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PorcentajeArancel = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AplicaITBS = table.Column<bool>(type: "bit", nullable: false),
                    AplicaSelectivo = table.Column<bool>(type: "bit", nullable: false),
                    PorctImptSelectivo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasArancelarias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionesImpuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PorcentajeITBIS = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    PorcentajeTasaAduanal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionesImpuestos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monedas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodigoIso = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Simbolo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    EsMonedaLocal = table.Column<bool>(type: "bit", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monedas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CodigoISO = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TasasCambio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonedaOrigenID = table.Column<int>(type: "int", nullable: false),
                    MonedaDestinoID = table.Column<int>(type: "int", nullable: false),
                    ValorTasa = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    FechaVigencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonedaId = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasasCambio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TasasCambio_Monedas_MonedaDestinoID",
                        column: x => x.MonedaDestinoID,
                        principalTable: "Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TasasCambio_Monedas_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "Monedas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TasasCambio_Monedas_MonedaOrigenID",
                        column: x => x.MonedaOrigenID,
                        principalTable: "Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Importadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRazonSocial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RNC = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    PaisID = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Importadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Importadores_Paises_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaisOrigenID = table.Column<int>(type: "int", nullable: false),
                    CategoriaArancelariaID = table.Column<int>(type: "int", nullable: false),
                    PesoUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Largo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Ancho = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Alto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UnidadDeMedidaID = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_CategoriasArancelarias_CategoriaArancelariaID",
                        column: x => x.CategoriaArancelariaID,
                        principalTable: "CategoriasArancelarias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_Paises_PaisOrigenID",
                        column: x => x.PaisOrigenID,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PaisOrigenID = table.Column<int>(type: "int", nullable: false),
                    MonedaPrincipalID = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proveedores_Monedas_MonedaPrincipalID",
                        column: x => x.MonedaPrincipalID,
                        principalTable: "Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proveedores_Paises_PaisOrigenID",
                        column: x => x.PaisOrigenID,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesImportacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroOrden = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PaisOrigenID = table.Column<int>(type: "int", nullable: false),
                    MonedaID = table.Column<int>(type: "int", nullable: false),
                    ImportadorID = table.Column<int>(type: "int", nullable: false),
                    ProveedorID = table.Column<int>(type: "int", nullable: false),
                    FechaOrden = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modalidadTransporte = table.Column<int>(type: "int", nullable: false),
                    EstadoProceso = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesImportacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesImportacion_Importadores_ImportadorID",
                        column: x => x.ImportadorID,
                        principalTable: "Importadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesImportacion_Monedas_MonedaID",
                        column: x => x.MonedaID,
                        principalTable: "Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenesImportacion_Paises_PaisOrigenID",
                        column: x => x.PaisOrigenID,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenesImportacion_Proveedores_ProveedorID",
                        column: x => x.ProveedorID,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesOrden",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenImportacionId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrecioUnitarioFOB = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MargenDeseado = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesOrden", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesOrden_OrdenesImportacion_OrdenImportacionId",
                        column: x => x.OrdenImportacionId,
                        principalTable: "OrdenesImportacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesOrden_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GastosImportacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenImportacionId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaGasto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonedaID = table.Column<int>(type: "int", nullable: false),
                    MetodoDistribucion = table.Column<int>(type: "int", nullable: false),
                    TipoGasto = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastosImportacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GastosImportacion_Monedas_MonedaID",
                        column: x => x.MonedaID,
                        principalTable: "Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GastosImportacion_OrdenesImportacion_OrdenImportacionId",
                        column: x => x.OrdenImportacionId,
                        principalTable: "OrdenesImportacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultadosCalculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenImportacionId = table.Column<int>(type: "int", nullable: false),
                    MonedaLocalId = table.Column<int>(type: "int", nullable: false),
                    TasaCambioUsadaId = table.Column<int>(type: "int", nullable: true),
                    FOBTotalOriginal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FOBTotalLocal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FleteTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SeguroTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CIFTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalArancel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalITBIS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoTotalImportacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PorcentajeITBIS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PorcentajeTasaAduanal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCalculo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadosCalculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultadosCalculo_Monedas_MonedaLocalId",
                        column: x => x.MonedaLocalId,
                        principalTable: "Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResultadosCalculo_OrdenesImportacion_OrdenImportacionId",
                        column: x => x.OrdenImportacionId,
                        principalTable: "OrdenesImportacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResultadosCalculo_TasasCambio_TasaCambioUsadaId",
                        column: x => x.TasaCambioUsadaId,
                        principalTable: "TasasCambio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesCalculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultadoCalculoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    CostoFOB = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Arancel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ITBIS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesCalculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesCalculo_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesCalculo_ResultadosCalculo_ResultadoCalculoId",
                        column: x => x.ResultadoCalculoId,
                        principalTable: "ResultadosCalculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasArancelarias_CodigoArancelio",
                table: "CategoriasArancelarias",
                column: "CodigoArancelio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionesImpuestos_Id",
                table: "ConfiguracionesImpuestos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCalculo_ProductoId",
                table: "DetallesCalculo",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCalculo_ResultadoCalculoId",
                table: "DetallesCalculo",
                column: "ResultadoCalculoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesOrden_OrdenImportacionId_ProductoId",
                table: "DetallesOrden",
                columns: new[] { "OrdenImportacionId", "ProductoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesOrden_ProductoId",
                table: "DetallesOrden",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_GastosImportacion_MonedaID",
                table: "GastosImportacion",
                column: "MonedaID");

            migrationBuilder.CreateIndex(
                name: "IX_GastosImportacion_OrdenImportacionId",
                table: "GastosImportacion",
                column: "OrdenImportacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Importadores_PaisID",
                table: "Importadores",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Importadores_RNC",
                table: "Importadores",
                column: "RNC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monedas_CodigoIso",
                table: "Monedas",
                column: "CodigoIso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesImportacion_ImportadorID",
                table: "OrdenesImportacion",
                column: "ImportadorID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesImportacion_MonedaID",
                table: "OrdenesImportacion",
                column: "MonedaID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesImportacion_PaisOrigenID",
                table: "OrdenesImportacion",
                column: "PaisOrigenID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesImportacion_ProveedorID",
                table: "OrdenesImportacion",
                column: "ProveedorID");

            migrationBuilder.CreateIndex(
                name: "IX_Paises_CodigoISO",
                table: "Paises",
                column: "CodigoISO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaArancelariaID",
                table: "Productos",
                column: "CategoriaArancelariaID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Codigo",
                table: "Productos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_PaisOrigenID",
                table: "Productos",
                column: "PaisOrigenID");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_MonedaPrincipalID",
                table: "Proveedores",
                column: "MonedaPrincipalID");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_PaisOrigenID",
                table: "Proveedores",
                column: "PaisOrigenID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosCalculo_MonedaLocalId",
                table: "ResultadosCalculo",
                column: "MonedaLocalId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosCalculo_OrdenImportacionId",
                table: "ResultadosCalculo",
                column: "OrdenImportacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosCalculo_TasaCambioUsadaId",
                table: "ResultadosCalculo",
                column: "TasaCambioUsadaId");

            migrationBuilder.CreateIndex(
                name: "IX_TasasCambio_MonedaDestinoID",
                table: "TasasCambio",
                column: "MonedaDestinoID");

            migrationBuilder.CreateIndex(
                name: "IX_TasasCambio_MonedaId",
                table: "TasasCambio",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_TasasCambio_MonedaOrigenID",
                table: "TasasCambio",
                column: "MonedaOrigenID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracionesImpuestos");

            migrationBuilder.DropTable(
                name: "DetallesCalculo");

            migrationBuilder.DropTable(
                name: "DetallesOrden");

            migrationBuilder.DropTable(
                name: "GastosImportacion");

            migrationBuilder.DropTable(
                name: "ResultadosCalculo");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "OrdenesImportacion");

            migrationBuilder.DropTable(
                name: "TasasCambio");

            migrationBuilder.DropTable(
                name: "CategoriasArancelarias");

            migrationBuilder.DropTable(
                name: "Importadores");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Monedas");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
