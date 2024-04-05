using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Models;

public partial class LaSurtidoraBuenPrecioIsContext : DbContext
{
    public LaSurtidoraBuenPrecioIsContext()
    {
    }

    public LaSurtidoraBuenPrecioIsContext(DbContextOptions<LaSurtidoraBuenPrecioIsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Caja> Cajas { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Credenciale> Credenciales { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleListadoDePedido> DetalleListadoDePedidos { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<ListadoPedido> ListadoPedidos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<MetodosPago> MetodosPagos { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<MovimientoCaja> MovimientoCajas { get; set; }

    public virtual DbSet<Presentacion> Presentacions { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<UnidadesMedida> UnidadesMedidas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Caja>(entity =>
        {
            entity.HasKey(e => e.IdCaja);

            entity.ToTable("Caja");

            entity.Property(e => e.IdCaja)
                .HasComment(" Identificador único de la caja.")
                .HasColumnName("ID_caja");
            entity.Property(e => e.Anio)
                .HasMaxLength(4)
                .HasComment("Año al que corresponde la caja.");
            entity.Property(e => e.Mes)
                .HasMaxLength(10)
                .HasComment("Mes al que corresponde la caja.");
            entity.Property(e => e.TotalCaja)
                .HasComment("Total de la caja.")
                .HasColumnType("money")
                .HasColumnName("Total_caja");
            entity.Property(e => e.TotalGastos)
                .HasComment("Total de gastos registrados en la caja.")
                .HasColumnType("money")
                .HasColumnName("Total_gastos");
            entity.Property(e => e.TotalIngresos)
                .HasComment("Total de ingresos registrados en la caja.")
                .HasColumnType("money")
                .HasColumnName("Total_ingresos");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.Property(e => e.IdCategoria)
                .HasComment("Identificador único para cada categoría de producto.")
                .HasColumnName("ID_categoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasComment("Una descripción de la categoría.");
            entity.Property(e => e.Estado).HasComment("El estado de la categoría (por ejemplo, activa, inactiva, etc.).");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .HasComment("El nombre de la categoría de producto.")
                .HasColumnName("Nombre_categoria");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra);

            entity.Property(e => e.IdCompra)
                .HasComment("Identificador único de la compra.")
                .HasColumnName("ID_compra");
            entity.Property(e => e.FechaCompra)
                .HasComment("Fecha de la compra.")
                .HasColumnType("date")
                .HasColumnName("Fecha_compra");
            entity.Property(e => e.FkMetodoPago)
                .HasComment(" Llave foránea que hace referencia al método de pago utilizado.")
                .HasColumnName("fk_metodo_pago");
            entity.Property(e => e.FkProveedor)
                .HasComment("Llave foránea que hace referencia al proveedor de la compra.")
                .HasColumnName("fk_proveedor");
            entity.Property(e => e.FkUsuario)
                .HasComment(" Llave foránea que hace referencia al empleado que realizó la compra.")
                .HasColumnName("fk_usuario");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(100)
                .HasComment("Observaciones adicionales sobre la compra.");
            entity.Property(e => e.Total)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.FkMetodoPagoNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.FkMetodoPago)
                .HasConstraintName("FK_Compras_Metodos_pago");

            entity.HasOne(d => d.FkProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.FkProveedor)
                .HasConstraintName("FK_Compras_Proveedores");

            entity.HasOne(d => d.FkUsuarioNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.FkUsuario)
                .HasConstraintName("FK_Compras_Usuario");
        });

        modelBuilder.Entity<Credenciale>(entity =>
        {
            entity.HasKey(e => e.IdCredenciales);

            entity.Property(e => e.IdCredenciales)
                .ValueGeneratedNever()
                .HasColumnName("id_credenciales");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FkRol).HasColumnName("fk_rol");
            entity.Property(e => e.Password)
                .HasMaxLength(25)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.FkRolNavigation).WithMany(p => p.Credenciales)
                .HasForeignKey(d => d.FkRol)
                .HasConstraintName("FK_Credenciales_Rol");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra);

            entity.ToTable("Detalle_compra");

            entity.Property(e => e.IdDetalleCompra)
                .HasComment("Identificador único del detalle de compra.")
                .HasColumnName("ID_detalle_compra");
            entity.Property(e => e.CantidadCompra)
                .HasComment(" Cantidad comprada del producto.")
                .HasColumnName("Cantidad_compra");
            entity.Property(e => e.Descuentos)
                .HasComment("Descuentos aplicados en el detalle de compra.")
                .HasColumnType("numeric(18, 2)");
            entity.Property(e => e.FkCompra)
                .HasComment("Llave foránea que hace referencia a la compra a la que pertenece el detalle.")
                .HasColumnName("fk_compra");
            entity.Property(e => e.FkProducto)
                .HasComment("Llave foránea que hace referencia al producto en la compra.")
                .HasColumnName("fk_producto");
            entity.Property(e => e.NoLote).HasColumnName("No_lote");
            entity.Property(e => e.PrecioSugeridoVenta)
                .HasComment("Precio sugerido de venta del producto.")
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("Precio_sugerido_venta");
            entity.Property(e => e.PrecioUnitarioCompra)
                .HasComment("Precio unitario de compra del producto.")
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("precio_unitario_compra");

            entity.HasOne(d => d.FkCompraNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.FkCompra)
                .HasConstraintName("FK_Detalle_compra_Compras");

            entity.HasOne(d => d.FkProductoNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.FkProducto)
                .HasConstraintName("FK_Detalle_compra_Productos");
        });

        modelBuilder.Entity<DetalleListadoDePedido>(entity =>
        {
            entity.HasKey(e => e.IdLsDetalleProducto);

            entity.ToTable("Detalle_listado_de_pedidos", tb => tb.HasComment("Identificador único para cada detalle de pedido."));

            entity.Property(e => e.IdLsDetalleProducto)
                .HasComment("Identificador único para cada tipo de presentación.")
                .HasColumnName("ID_ls_detalle_producto");
            entity.Property(e => e.CantidadAComprar)
                .HasComment(" La cantidad del producto que se va a comprar.")
                .HasColumnName("Cantidad_a_comprar");
            entity.Property(e => e.FkListadoPedido)
                .HasComment("Llave foránea que se relaciona con el listado de pedidos al que pertenece el detalle.")
                .HasColumnName("fk_listado_pedido");
            entity.Property(e => e.FkProducto3)
                .HasComment("Llave foránea que se relaciona con el producto pedido.")
                .HasColumnName("fk_Producto3");

            entity.HasOne(d => d.FkListadoPedidoNavigation).WithMany(p => p.DetalleListadoDePedidos)
                .HasForeignKey(d => d.FkListadoPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_listado_de_pedidos_Listado_pedidos");

            entity.HasOne(d => d.FkProducto3Navigation).WithMany(p => p.DetalleListadoDePedidos)
                .HasForeignKey(d => d.FkProducto3)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_listado_de_pedidos_Productos");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta);

            entity.ToTable("Detalle_ventas");

            entity.Property(e => e.IdDetalleVenta)
                .HasComment("Identificador único para cada detalle de venta.")
                .HasColumnName("ID_detalle_venta");
            entity.Property(e => e.CantidadProducto).HasColumnName("cantidad_producto");
            entity.Property(e => e.Descuento)
                .HasDefaultValueSql("((0))")
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("descuento");
            entity.Property(e => e.FkIdProducto1)
                .HasComment(" Llave foránea que se relaciona con el producto vendido.")
                .HasColumnName("fk_id_producto1");
            entity.Property(e => e.FkVenta)
                .HasComment("Llave foránea que se relaciona con la venta a la que pertenece el detalle.")
                .HasColumnName("fk_venta");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("precio_unitario");

            entity.HasOne(d => d.FkIdProducto1Navigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.FkIdProducto1)
                .HasConstraintName("FK_Detalle_ventas_Productos");

            entity.HasOne(d => d.FkVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.FkVenta)
                .HasConstraintName("FK_Detalle_ventas_Ventas");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK_Inventario_1");

            entity.ToTable("Inventario");

            entity.Property(e => e.IdInventario)
                .HasComment("Identificador único para cada registro en el inventario.")
                .HasColumnName("ID_inventario");
            entity.Property(e => e.FechaUltimaEntrada)
                .HasComment(" La fecha de la última entrada de inventario.")
                .HasColumnType("date")
                .HasColumnName("fecha_ultima_entrada");
            entity.Property(e => e.FechaUltimaSalida)
                .HasComment("La fecha de la última salida de inventario")
                .HasColumnType("date")
                .HasColumnName("fecha_ultima_salida");
            entity.Property(e => e.FkProducto2)
                .HasComment("Llave foránea que se relaciona con el producto en el inventario.")
                .HasColumnName("fk_producto2");
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("precio_venta");
            entity.Property(e => e.TotalStock)
                .HasComment("La cantidad total de stock disponible del producto en el inventario.")
                .HasColumnName("total_stock");

            entity.HasOne(d => d.FkProducto2Navigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.FkProducto2)
                .HasConstraintName("FK_Inventario_Productos");
        });

        modelBuilder.Entity<ListadoPedido>(entity =>
        {
            entity.HasKey(e => e.IdListado);

            entity.ToTable("Listado_pedidos");

            entity.Property(e => e.IdListado)
                .HasComment("Identificador único para cada listado de pedidos.")
                .HasColumnName("ID_listado");
            entity.Property(e => e.Estado).HasComment("El estado del listado de pedidos. puede ser completado, en proceso, en espera ente otras");
            entity.Property(e => e.FKEmpleado)
                .HasComment("Llave foránea que se relaciona con el empleado que creó el listado.")
                .HasColumnName("fK_empleado");
            entity.Property(e => e.FechaCreacion)
                .HasComment("La fecha en que se creó el listado de pedidos.")
                .HasColumnType("date")
                .HasColumnName("Fecha_creacion");

            entity.HasOne(d => d.FKEmpleadoNavigation).WithMany(p => p.ListadoPedidos)
                .HasForeignKey(d => d.FKEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Listado_pedidos_Empleados");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarcas);

            entity.Property(e => e.IdMarcas)
                .HasComment("Identificador único para cada marca.")
                .HasColumnName("ID_marcas");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .HasComment("Una descripción de la marca. Este campo puede tneer como maximo 150 caracteres ");
            entity.Property(e => e.Estado).HasComment("El estado de la marca. si esta activo o desactivo");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(80)
                .HasComment("El nombre de la marca.")
                .HasColumnName("Nombre_marca");
        });

        modelBuilder.Entity<MetodosPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago);

            entity.ToTable("Metodos_pago");

            entity.Property(e => e.IdMetodoPago)
                .HasComment("Identificador único del método de pago.")
                .HasColumnName("ID_metodo_pago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasComment("Descripción del método de pago.");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasComment("Nombre del método de pago.");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento);

            entity.Property(e => e.IdMovimiento)
                .HasComment("Identificador único del tipo de movimiento.")
                .HasColumnName("ID_movimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasComment("Nombre del tipo de movimiento.");
        });

        modelBuilder.Entity<MovimientoCaja>(entity =>
        {
            entity.HasKey(e => e.IdMovCaja);

            entity.ToTable("Movimiento_caja");

            entity.Property(e => e.IdMovCaja)
                .HasComment("Identificador único del movimiento de caja.")
                .HasColumnName("ID_mov_caja");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasComment("Descripción del movimiento de caja.");
            entity.Property(e => e.Fecha)
                .HasComment("Fecha del movimiento de caja.")
                .HasColumnType("date");
            entity.Property(e => e.FkTipoMovimiento)
                .HasComment("Llave foránea que hace referencia al tipo de movimiento.")
                .HasColumnName("fk_tipo_movimiento");
            entity.Property(e => e.MontoTotal)
                .HasComment("Monto total del movimiento de caja.")
                .HasColumnType("money")
                .HasColumnName("Monto_total");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(150)
                .HasComment("Descripción del movimiento de caja.");

            entity.HasOne(d => d.FkTipoMovimientoNavigation).WithMany(p => p.MovimientoCajas)
                .HasForeignKey(d => d.FkTipoMovimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimiento_caja_Movimientos");
        });

        modelBuilder.Entity<Presentacion>(entity =>
        {
            entity.HasKey(e => e.IdPresentacion);

            entity.ToTable("Presentacion");

            entity.Property(e => e.IdPresentacion)
                .HasComment("Identificador único para cada tipo de presentación.")
                .HasColumnName("ID_presentacion");
            entity.Property(e => e.NombrePresentacion)
                .HasMaxLength(50)
                .HasComment("El nombre o tipo de presentación del producto.")
                .HasColumnName("Nombre_presentacion");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProductos);

            entity.Property(e => e.IdProductos)
                .HasComment("Identificador único para cada producto.")
                .HasColumnName("ID_productos");
            entity.Property(e => e.CodigoBarras)
                .HasMaxLength(20)
                .HasComment("El código de barras del producto.")
                .HasColumnName("Codigo_barras");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasComment("Una descripción del producto.");
            entity.Property(e => e.Estado).HasComment("El estado del producto (por ejemplo, disponible, agotado, etc.).");
            entity.Property(e => e.FkCategoria)
                .HasComment("Llave foránea que se relaciona con la categoría del producto.")
                .HasColumnName("fk_categoria");
            entity.Property(e => e.FkMarca)
                .HasComment("Llave foránea que se relaciona con la marca del producto.")
                .HasColumnName("fk_marca");
            entity.Property(e => e.FkPresentacion)
                .HasComment(" Llave foránea que se relaciona con la presentación del producto.")
                .HasColumnName("fk_presentacion");
            entity.Property(e => e.FkUnidadMedida)
                .HasComment("Llave foránea que se relaciona con la unidad de medida del producto.")
                .HasColumnName("fk_unidad_medida");
            entity.Property(e => e.Imagen).HasComment("Una imagen del producto (generalmente como una referencia).");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(50)
                .HasComment("El nombre del producto.")
                .HasColumnName("Nombre_producto");
            entity.Property(e => e.Tamanio)
                .HasComment("Tamaño del producto.")
                .HasColumnType("numeric(4, 2)");

            entity.HasOne(d => d.FkCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.FkCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Categorias");

            entity.HasOne(d => d.FkMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.FkMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Marcas");

            entity.HasOne(d => d.FkPresentacionNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.FkPresentacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Presentacion");

            entity.HasOne(d => d.FkUnidadMedidaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.FkUnidadMedida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Unidades_medidas");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor);

            entity.Property(e => e.IdProveedor)
                .HasComment("Identificador único para cada proveedor.")
                .HasColumnName("ID_proveedor");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasComment("Cualquier descripción adicional del proveedor. Maximo 100 caracteres.");
            entity.Property(e => e.DiaEntrega)
                .HasMaxLength(25)
                .HasComment("El día de entrega preferido por el proveedor.")
                .HasColumnName("Dia_entrega");
            entity.Property(e => e.DiaVisita)
                .HasMaxLength(25)
                .HasComment("El día en que se realiza la visita al proveedor.")
                .HasColumnName("Dia_visita");
            entity.Property(e => e.Estado).HasComment("El estado del proveedor. puede ser activo o inactivo  entre otras");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(50)
                .HasComment("El nombre del proveedor.")
                .HasColumnName("Nombre_proveedor");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasComment("El número de teléfono del proveedor.");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK_Puestos");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol)
                .HasComment("Identificador único para cada puesto en la tienda.")
                .HasColumnName("ID_rol");
            entity.Property(e => e.Detalles)
                .HasMaxLength(50)
                .HasComment("Cualquier información adicional sobre el puesto.");
            entity.Property(e => e.NombrePuesto)
                .HasMaxLength(25)
                .HasComment("El nombre del puesto.")
                .HasColumnName("Nombre_puesto");
        });

        modelBuilder.Entity<UnidadesMedida>(entity =>
        {
            entity.HasKey(e => e.IdMedicion);

            entity.ToTable("Unidades_medidas");

            entity.Property(e => e.IdMedicion)
                .HasComment("Identificador único para cada unidad de medida.")
                .HasColumnName("ID_medicion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .HasComment("Descripción de la unidad de medida.");
            entity.Property(e => e.Prefijo)
                .HasMaxLength(6)
                .HasComment("Prefijo asociado a la unidad de medida.")
                .HasColumnName("prefijo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_Empleados");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedOnAdd()
                .HasComment("Identificador único para cada empleado.")
                .HasColumnName("ID_usuario");
            entity.Property(e => e.Email)
                .HasComment("Cualquier otro nombre del empleado.")
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro).HasColumnName("fecha_registro");
            entity.Property(e => e.OtrosNombres)
                .HasMaxLength(50)
                .HasComment("Cualquier otro nombre del empleado.")
                .HasColumnName("otros_nombres");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .HasComment("El primer apellido del empleado.")
                .HasColumnName("primer_apellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(30)
                .HasComment("El primer apellido del empleado.")
                .HasColumnName("primer_nombre");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .HasComment("El segundo apellido del empleado.")
                .HasColumnName("segundo_apellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(30)
                .HasComment("El segundo apellido del empleado.")
                .HasColumnName("segundo_nombre");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Credenciales");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta);

            entity.Property(e => e.IdVenta)
                .HasComment("Identificador único para cada venta.")
                .HasColumnName("ID_venta");
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasComment("La fecha en que se realizó la venta.\r\n")
                .HasColumnType("date")
                .HasColumnName("fecha_Venta");
            entity.Property(e => e.FkUsuario1)
                .HasComment("Llave foránea que se relaciona con el empleado que realizó la venta.")
                .HasColumnName("fk_usuario1");
            entity.Property(e => e.Total)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.FkUsuario1Navigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.FkUsuario1)
                .HasConstraintName("FK_Ventas_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
