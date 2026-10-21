using System;
using System.Collections.Generic;
using EcomConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomConnect.Data;

public partial class EcomConnectContext : DbContext
{
    public EcomConnectContext(DbContextOptions<EcomConnectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bodega> Bodegas { get; set; }

    public virtual DbSet<CampanaMarketing> CampanaMarketings { get; set; }

    public virtual DbSet<Canton> Cantons { get; set; }

    public virtual DbSet<CatalogoGenero> CatalogoGeneros { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Correo> Correos { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Distrito> Distritos { get; set; }

    public virtual DbSet<EstadoCliente> EstadoClientes { get; set; }

    public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoDetalle> PedidoDetalles { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Preferencium> Preferencia { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Promocion> Promocions { get; set; }

    public virtual DbSet<Provincium> Provincia { get; set; }

    public virtual DbSet<QuejaSolicitud> QuejaSolicituds { get; set; }

    public virtual DbSet<RastreoPedido> RastreoPedidos { get; set; }

    public virtual DbSet<Segmento> Segmentos { get; set; }

    public virtual DbSet<Telefono> Telefonos { get; set; }

    public virtual DbSet<TipoCorreo> TipoCorreos { get; set; }

    public virtual DbSet<TipoDireccion> TipoDireccions { get; set; }

    public virtual DbSet<TipoPersona> TipoPersonas { get; set; }

    public virtual DbSet<TipoTelefono> TipoTelefonos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bodega>(entity =>
        {
            entity.HasKey(e => e.IdBodega).HasName("PK_bodega_idBodega");

            entity.ToTable("bodega", "ecomconnect");

            entity.Property(e => e.IdBodega).HasColumnName("idBodega");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.TiempoProcesamientoHoras).HasColumnName("Tiempo_Procesamiento_Horas");
            entity.Property(e => e.Ubicacion).HasMaxLength(255);
        });

        modelBuilder.Entity<CampanaMarketing>(entity =>
        {
            entity.HasKey(e => e.IdCampanaMarketing).HasName("PK_campana_marketing_idCampana_marketing");

            entity.ToTable("campana_marketing", "ecomconnect");

            entity.HasIndex(e => new { e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }, "fk_Campaña_marketing_Cliente1_idx");

            entity.HasIndex(e => e.PromocionIdPromocion, "fk_Campaña_marketing_Promocion1_idx");

            entity.Property(e => e.IdCampanaMarketing).HasColumnName("idCampana_marketing");
            entity.Property(e => e.ClienteIdCliente).HasColumnName("Cliente_idCliente");
            entity.Property(e => e.ClientePersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Cliente_Persona_Cedula");
            entity.Property(e => e.ClientePersonaTipoPersonaIdtipoPersona).HasColumnName("Cliente_Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.DescripcionCampanamarketing)
                .HasMaxLength(255)
                .HasColumnName("descripcion_campanamarketing");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PromocionIdPromocion).HasColumnName("Promocion_idPromocion");

            entity.HasOne(d => d.PromocionIdPromocionNavigation).WithMany(p => p.CampanaMarketings)
                .HasForeignKey(d => d.PromocionIdPromocion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("campana_marketing$fk_Campaña_marketing_Promocion1");

            entity.HasOne(d => d.Cliente).WithMany(p => p.CampanaMarketings)
                .HasForeignKey(d => new { d.ClienteIdCliente, d.ClientePersonaCedula, d.ClientePersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("campana_marketing$fk_Campaña_marketing_Cliente1");
        });

        modelBuilder.Entity<Canton>(entity =>
        {
            entity.HasKey(e => new { e.IdCanton, e.ProvinciaIdProvincia }).HasName("PK_canton_idCanton");

            entity.ToTable("canton", "ecomconnect");

            entity.HasIndex(e => e.ProvinciaIdProvincia, "fk_Canton_Provincia1_idx");

            entity.Property(e => e.IdCanton)
                .ValueGeneratedOnAdd()
                .HasColumnName("idCanton");
            entity.Property(e => e.ProvinciaIdProvincia).HasColumnName("Provincia_idProvincia");
            entity.Property(e => e.DescipcionCanton)
                .HasMaxLength(50)
                .HasColumnName("Descipcion_Canton");

            entity.HasOne(d => d.ProvinciaIdProvinciaNavigation).WithMany(p => p.Cantons)
                .HasForeignKey(d => d.ProvinciaIdProvincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("canton$fk_Canton_Provincia1");
        });

        modelBuilder.Entity<CatalogoGenero>(entity =>
        {
            entity.HasKey(e => e.IdCatalogoGenero).HasName("PK_catalogo_genero_idCatalogo_Genero");

            entity.ToTable("catalogo_genero", "ecomconnect");

            entity.Property(e => e.IdCatalogoGenero).HasColumnName("idCatalogo_Genero");
            entity.Property(e => e.DescripcionGenero)
                .HasMaxLength(45)
                .HasColumnName("Descripcion_Genero");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => new { e.IdCliente, e.PersonaCedula, e.PersonaTipoPersonaIdtipoPersona }).HasName("PK_cliente_idCliente");

            entity.ToTable("cliente", "ecomconnect");

            entity.HasIndex(e => new { e.PersonaCedula, e.PersonaTipoPersonaIdtipoPersona }, "fk_Cliente_Persona1_idx");

            entity.Property(e => e.IdCliente)
                .ValueGeneratedOnAdd()
                .HasColumnName("idCliente");
            entity.Property(e => e.PersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Persona_Cedula");
            entity.Property(e => e.PersonaTipoPersonaIdtipoPersona).HasColumnName("Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.Calificacion).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.FechaBaja).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .HasColumnName("Metodo_Pago");

            entity.HasOne(d => d.Persona).WithMany(p => p.Clientes)
                .HasForeignKey(d => new { d.PersonaCedula, d.PersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cliente$fk_Cliente_Persona1");
        });

        modelBuilder.Entity<Correo>(entity =>
        {
            entity.HasKey(e => new { e.Correo1, e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }).HasName("PK_correo_Correo");

            entity.ToTable("correo", "ecomconnect");

            entity.HasIndex(e => new { e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }, "fk_Correo_Cliente1_idx");

            entity.HasIndex(e => e.TipoCorreoIdTipoCorreo, "fk_Correo_Tipo_Correo1_idx");

            entity.Property(e => e.Correo1)
                .HasMaxLength(250)
                .HasColumnName("Correo");
            entity.Property(e => e.ClienteIdCliente).HasColumnName("Cliente_idCliente");
            entity.Property(e => e.ClientePersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Cliente_Persona_Cedula");
            entity.Property(e => e.ClientePersonaTipoPersonaIdtipoPersona).HasColumnName("Cliente_Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.TipoCorreoIdTipoCorreo).HasColumnName("Tipo_Correo_idTipo_Correo");

            entity.HasOne(d => d.TipoCorreoIdTipoCorreoNavigation).WithMany(p => p.Correos)
                .HasForeignKey(d => d.TipoCorreoIdTipoCorreo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("correo$fk_Correo_Tipo_Correo1");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Correos)
                .HasForeignKey(d => new { d.ClienteIdCliente, d.ClientePersonaCedula, d.ClientePersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("correo$fk_Correo_Cliente1");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => new { e.DistritoIdDistrito, e.DistritoCantonIdCanton, e.DistritoCantonProvinciaIdProvincia, e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }).HasName("PK_direccion_Distrito_idDistrito");

            entity.ToTable("direccion", "ecomconnect");

            entity.HasIndex(e => new { e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }, "fk_Direccion_Cliente1_idx");

            entity.HasIndex(e => e.TipoDireccionIdTipoDireccion, "fk_Direccion_Tipo_Direccion1_idx");

            entity.Property(e => e.DistritoIdDistrito).HasColumnName("Distrito_idDistrito");
            entity.Property(e => e.DistritoCantonIdCanton).HasColumnName("Distrito_Canton_idCanton");
            entity.Property(e => e.DistritoCantonProvinciaIdProvincia).HasColumnName("Distrito_Canton_Provincia_idProvincia");
            entity.Property(e => e.ClienteIdCliente).HasColumnName("Cliente_idCliente");
            entity.Property(e => e.ClientePersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Cliente_Persona_Cedula");
            entity.Property(e => e.ClientePersonaTipoPersonaIdtipoPersona).HasColumnName("Cliente_Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.Detalle).HasMaxLength(500);
            entity.Property(e => e.TipoDireccionIdTipoDireccion).HasColumnName("Tipo_Direccion_idTipo_Direccion");

            entity.HasOne(d => d.TipoDireccionIdTipoDireccionNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.TipoDireccionIdTipoDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("direccion$fk_Direccion_Tipo_Direccion1");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Direccions)
                .HasForeignKey(d => new { d.ClienteIdCliente, d.ClientePersonaCedula, d.ClientePersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("direccion$fk_Direccion_Cliente1");

            entity.HasOne(d => d.Distrito).WithMany(p => p.Direccions)
                .HasForeignKey(d => new { d.DistritoIdDistrito, d.DistritoCantonIdCanton, d.DistritoCantonProvinciaIdProvincia })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("direccion$fk_Direccion_Distrito1");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => new { e.IdDistrito, e.CantonIdCanton, e.CantonProvinciaIdProvincia }).HasName("PK_distrito_idDistrito");

            entity.ToTable("distrito", "ecomconnect");

            entity.HasIndex(e => new { e.CantonIdCanton, e.CantonProvinciaIdProvincia }, "fk_Distrito_Canton1_idx");

            entity.Property(e => e.IdDistrito)
                .ValueGeneratedOnAdd()
                .HasColumnName("idDistrito");
            entity.Property(e => e.CantonIdCanton).HasColumnName("Canton_idCanton");
            entity.Property(e => e.CantonProvinciaIdProvincia).HasColumnName("Canton_Provincia_idProvincia");
            entity.Property(e => e.CodigoPostal).HasColumnName("Codigo_Postal");
            entity.Property(e => e.DescripcionDistrito)
                .HasMaxLength(50)
                .HasColumnName("Descripcion_Distrito");

            entity.HasOne(d => d.Canton).WithMany(p => p.Distritos)
                .HasForeignKey(d => new { d.CantonIdCanton, d.CantonProvinciaIdProvincia })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("distrito$fk_Distrito_Canton1");
        });

        modelBuilder.Entity<EstadoCliente>(entity =>
        {
            entity.HasKey(e => e.IdEstadoCliente).HasName("PK_estado_cliente_idEstado_Cliente");

            entity.ToTable("estado_cliente", "ecomconnect");

            entity.HasIndex(e => new { e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }, "fk_Estado_Cliente_Cliente1_idx");

            entity.Property(e => e.IdEstadoCliente).HasColumnName("idEstado_Cliente");
            entity.Property(e => e.ClienteIdCliente).HasColumnName("Cliente_idCliente");
            entity.Property(e => e.ClientePersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Cliente_Persona_Cedula");
            entity.Property(e => e.ClientePersonaTipoPersonaIdtipoPersona).HasColumnName("Cliente_Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.FechaCambioEstado).HasColumnName("Fecha_CambioEstado");

            entity.HasOne(d => d.Cliente).WithMany(p => p.EstadoClientes)
                .HasForeignKey(d => new { d.ClienteIdCliente, d.ClientePersonaCedula, d.ClientePersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estado_cliente$fk_Estado_Cliente_Cliente1");
        });

        modelBuilder.Entity<EstadoPedido>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPedido).HasName("PK_estado_pedido_idEstado_Pedido");

            entity.ToTable("estado_pedido", "ecomconnect");

            entity.Property(e => e.IdEstadoPedido).HasColumnName("idEstado_Pedido");
            entity.Property(e => e.DescripcionEstado)
                .HasMaxLength(45)
                .HasColumnName("Descripcion_Estado");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK_factura_idFactura");

            entity.ToTable("factura", "ecomconnect");

            entity.HasIndex(e => e.MetodoPagoIdMetodoPago, "fk_Factura_Metodo_Pago1_idx");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.FechaFactura).HasColumnName("Fecha_Factura");
            entity.Property(e => e.MetodoPagoIdMetodoPago).HasColumnName("Metodo_Pago_idMetodo_Pago");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.MetodoPagoIdMetodoPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.MetodoPagoIdMetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("factura$fk_Factura_Metodo_Pago1");
        });

        modelBuilder.Entity<FacturaDetalle>(entity =>
        {
            entity.HasKey(e => new { e.IdFacturaDetalle, e.ProductoIdProducto, e.ProductoBodegaIdBodega }).HasName("PK_factura_detalle_idFactura_Detalle");

            entity.ToTable("factura_detalle", "ecomconnect");

            entity.HasIndex(e => e.FacturaIdFactura, "fk_Factura_Detalle_Factura1_idx");

            entity.HasIndex(e => new { e.ProductoIdProducto, e.ProductoBodegaIdBodega }, "fk_Factura_Detalle_Producto1_idx");

            entity.Property(e => e.IdFacturaDetalle)
                .ValueGeneratedOnAdd()
                .HasColumnName("idFactura_Detalle");
            entity.Property(e => e.ProductoIdProducto).HasColumnName("Producto_idProducto");
            entity.Property(e => e.ProductoBodegaIdBodega).HasColumnName("Producto_Bodega_idBodega");
            entity.Property(e => e.FacturaIdFactura).HasColumnName("Factura_idFactura");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Precio_Unitario");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.FacturaIdFacturaNavigation).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.FacturaIdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("factura_detalle$fk_Factura_Detalle_Factura1");

            entity.HasOne(d => d.Producto).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => new { d.ProductoIdProducto, d.ProductoBodegaIdBodega })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("factura_detalle$fk_Factura_Detalle_Producto1");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK_metodo_pago_idMetodo_Pago");

            entity.ToTable("metodo_pago", "ecomconnect");

            entity.Property(e => e.IdMetodoPago).HasColumnName("idMetodo_Pago");
            entity.Property(e => e.DescripcionMetodopago)
                .HasMaxLength(50)
                .HasColumnName("descripcion_metodopago");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => new { e.IdPedido, e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }).HasName("PK_pedido_idPedido");

            entity.ToTable("pedido", "ecomconnect");

            entity.HasIndex(e => new { e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }, "fk_Pedido_Cliente1_idx");

            entity.Property(e => e.IdPedido)
                .ValueGeneratedOnAdd()
                .HasColumnName("idPedido");
            entity.Property(e => e.ClienteIdCliente).HasColumnName("Cliente_idCliente");
            entity.Property(e => e.ClientePersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Cliente_Persona_Cedula");
            entity.Property(e => e.ClientePersonaTipoPersonaIdtipoPersona).HasColumnName("Cliente_Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.FechaPedido).HasColumnName("Fecha_Pedido");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => new { d.ClienteIdCliente, d.ClientePersonaCedula, d.ClientePersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pedido$fk_Pedido_Cliente1");
        });

        modelBuilder.Entity<PedidoDetalle>(entity =>
        {
            entity.HasKey(e => new { e.IdPedidoDetalle, e.ProductoIdProducto, e.ProductoBodegaIdBodega }).HasName("PK_pedido_detalle_idPedido_Detalle");

            entity.ToTable("pedido_detalle", "ecomconnect");

            entity.HasIndex(e => new { e.PedidoIdPedido, e.PedidoClienteIdCliente, e.PedidoClientePersonaCedula, e.PedidoClientePersonaTipoPersonaIdtipoPersona }, "fk_Pedido_Detalle_Pedido1_idx");

            entity.HasIndex(e => new { e.ProductoIdProducto, e.ProductoBodegaIdBodega }, "fk_Pedido_Detalle_Producto1_idx");

            entity.Property(e => e.IdPedidoDetalle)
                .ValueGeneratedOnAdd()
                .HasColumnName("idPedido_Detalle");
            entity.Property(e => e.ProductoIdProducto).HasColumnName("Producto_idProducto");
            entity.Property(e => e.ProductoBodegaIdBodega).HasColumnName("Producto_Bodega_idBodega");
            entity.Property(e => e.PedidoClienteIdCliente).HasColumnName("Pedido_Cliente_idCliente");
            entity.Property(e => e.PedidoClientePersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Pedido_Cliente_Persona_Cedula");
            entity.Property(e => e.PedidoClientePersonaTipoPersonaIdtipoPersona).HasColumnName("Pedido_Cliente_Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.PedidoIdPedido).HasColumnName("Pedido_idPedido");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Precio_Unitario");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Producto).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => new { d.ProductoIdProducto, d.ProductoBodegaIdBodega })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pedido_detalle$fk_Pedido_Detalle_Producto1");

            entity.HasOne(d => d.Pedido).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => new { d.PedidoIdPedido, d.PedidoClienteIdCliente, d.PedidoClientePersonaCedula, d.PedidoClientePersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pedido_detalle$fk_Pedido_Detalle_Pedido1");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => new { e.Cedula, e.TipoPersonaIdtipoPersona }).HasName("PK_persona_Cedula");

            entity.ToTable("persona", "ecomconnect");

            entity.HasIndex(e => e.CatalogoGeneroIdCatalogoGenero, "fk_Persona_Catalogo_Genero1_idx");

            entity.HasIndex(e => e.TipoPersonaIdtipoPersona, "fk_Persona_tipo_persona1_idx");

            entity.HasIndex(e => e.Cedula, "persona$Cedula_UNIQUE").IsUnique();

            entity.HasIndex(e => e.IdPersona, "persona$idPersona_UNIQUE").IsUnique();

            entity.Property(e => e.Cedula).HasMaxLength(10);
            entity.Property(e => e.TipoPersonaIdtipoPersona).HasColumnName("tipo_persona_idtipo_persona");
            entity.Property(e => e.Apellido1).HasMaxLength(30);
            entity.Property(e => e.Apellido2).HasMaxLength(30);
            entity.Property(e => e.CatalogoGeneroIdCatalogoGenero).HasColumnName("Catalogo_Genero_idCatalogo_Genero");
            entity.Property(e => e.FechaNacimiento).HasColumnName("Fecha_Nacimiento");
            entity.Property(e => e.IdPersona)
                .ValueGeneratedOnAdd()
                .HasColumnName("idPersona");
            entity.Property(e => e.Nombre).HasMaxLength(30);

            entity.HasOne(d => d.CatalogoGeneroIdCatalogoGeneroNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.CatalogoGeneroIdCatalogoGenero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("persona$fk_Persona_Catalogo_Genero1");

            entity.HasOne(d => d.TipoPersonaIdtipoPersonaNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.TipoPersonaIdtipoPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("persona$fk_Persona_tipo_persona1");
        });

        modelBuilder.Entity<Preferencium>(entity =>
        {
            entity.HasKey(e => e.IdPreferencia).HasName("PK_preferencia_idPreferencia");

            entity.ToTable("preferencia", "ecomconnect");

            entity.HasIndex(e => new { e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }, "fk_Preferencia_Cliente1_idx");

            entity.Property(e => e.IdPreferencia).HasColumnName("idPreferencia");
            entity.Property(e => e.ClienteIdCliente).HasColumnName("Cliente_idCliente");
            entity.Property(e => e.ClientePersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Cliente_Persona_Cedula");
            entity.Property(e => e.ClientePersonaTipoPersonaIdtipoPersona).HasColumnName("Cliente_Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.DescripcionPreferencia)
                .HasMaxLength(150)
                .HasColumnName("descripcion_preferencia");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Preferencia)
                .HasForeignKey(d => new { d.ClienteIdCliente, d.ClientePersonaCedula, d.ClientePersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("preferencia$fk_Preferencia_Cliente1");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => new { e.IdProducto, e.BodegaIdBodega }).HasName("PK_producto_idProducto");

            entity.ToTable("producto", "ecomconnect");

            entity.HasIndex(e => e.BodegaIdBodega, "fk_Producto_Bodega1_idx");

            entity.Property(e => e.IdProducto)
                .ValueGeneratedOnAdd()
                .HasColumnName("idProducto");
            entity.Property(e => e.BodegaIdBodega).HasColumnName("Bodega_idBodega");
            entity.Property(e => e.BackOrder).HasColumnName("Back_Order");
            entity.Property(e => e.CantidadDisponible).HasColumnName("Cantidad_Disponible");
            entity.Property(e => e.DescripcionProducto)
                .HasMaxLength(50)
                .HasColumnName("descripcion_producto");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PuntoReorden).HasColumnName("Punto_Reorden");

            entity.HasOne(d => d.BodegaIdBodegaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.BodegaIdBodega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("producto$fk_Producto_Bodega1");
        });

        modelBuilder.Entity<Promocion>(entity =>
        {
            entity.HasKey(e => e.IdPromocion).HasName("PK_promocion_idPromocion");

            entity.ToTable("promocion", "ecomconnect");

            entity.Property(e => e.IdPromocion).HasColumnName("idPromocion");
            entity.Property(e => e.DescripcionPromocion)
                .HasMaxLength(255)
                .HasColumnName("descripcion_promocion");
            entity.Property(e => e.Descuento)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("descuento");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Provincium>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PK_provincia_idProvincia");

            entity.ToTable("provincia", "ecomconnect");

            entity.Property(e => e.IdProvincia).HasColumnName("idProvincia");
            entity.Property(e => e.DescripcionProvincia)
                .HasMaxLength(50)
                .HasColumnName("Descripcion_Provincia");
        });

        modelBuilder.Entity<QuejaSolicitud>(entity =>
        {
            entity.HasKey(e => e.IdQuejaSolicitud).HasName("PK_queja_solicitud_idQueja_solicitud");

            entity.ToTable("queja_solicitud", "ecomconnect");

            entity.HasIndex(e => new { e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }, "fk_Queja_solicitud_Cliente1_idx");

            entity.Property(e => e.IdQuejaSolicitud).HasColumnName("idQueja_solicitud");
            entity.Property(e => e.ClienteIdCliente).HasColumnName("Cliente_idCliente");
            entity.Property(e => e.ClientePersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Cliente_Persona_Cedula");
            entity.Property(e => e.ClientePersonaTipoPersonaIdtipoPersona).HasColumnName("Cliente_Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.DescripcionQuejasolicitud)
                .HasMaxLength(500)
                .HasColumnName("descripcion_quejasolicitud");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasPrecision(0)
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Respuesta)
                .HasMaxLength(500)
                .HasColumnName("respuesta");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasColumnName("tipo");

            entity.HasOne(d => d.Cliente).WithMany(p => p.QuejaSolicituds)
                .HasForeignKey(d => new { d.ClienteIdCliente, d.ClientePersonaCedula, d.ClientePersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("queja_solicitud$fk_Queja_solicitud_Cliente1");
        });

        modelBuilder.Entity<RastreoPedido>(entity =>
        {
            entity.HasKey(e => e.IdRastreo).HasName("PK_rastreo_pedido_idRastreo");

            entity.ToTable("rastreo_pedido", "ecomconnect");

            entity.HasIndex(e => new { e.PedidoIdPedido, e.PedidoClienteIdCliente, e.PedidoClientePersonaCedula, e.PedidoClientePersonaTipoPersonaIdtipoPersona }, "fk_Rastreo_Pedido_Pedido1_idx");

            entity.HasIndex(e => e.EstadoPedidoIdEstadoPedido, "fk_Ratreo_Pedido_Estado_Pedido1_idx");

            entity.Property(e => e.IdRastreo).HasColumnName("idRastreo");
            entity.Property(e => e.EstadoPedidoIdEstadoPedido).HasColumnName("Estado_Pedido_idEstado_Pedido");
            entity.Property(e => e.FechaActualizacion).HasColumnName("Fecha_Actualizacion");
            entity.Property(e => e.PedidoClienteIdCliente).HasColumnName("Pedido_Cliente_idCliente");
            entity.Property(e => e.PedidoClientePersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Pedido_Cliente_Persona_Cedula");
            entity.Property(e => e.PedidoClientePersonaTipoPersonaIdtipoPersona).HasColumnName("Pedido_Cliente_Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.PedidoIdPedido).HasColumnName("Pedido_idPedido");

            entity.HasOne(d => d.EstadoPedidoIdEstadoPedidoNavigation).WithMany(p => p.RastreoPedidos)
                .HasForeignKey(d => d.EstadoPedidoIdEstadoPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rastreo_pedido$fk_Ratreo_Pedido_Estado_Pedido1");

            entity.HasOne(d => d.Pedido).WithMany(p => p.RastreoPedidos)
                .HasForeignKey(d => new { d.PedidoIdPedido, d.PedidoClienteIdCliente, d.PedidoClientePersonaCedula, d.PedidoClientePersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rastreo_pedido$fk_Rastreo_Pedido_Pedido1");
        });

        modelBuilder.Entity<Segmento>(entity =>
        {
            entity.HasKey(e => new { e.IdSegmento, e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }).HasName("PK_segmento_idSegmento");

            entity.ToTable("segmento", "ecomconnect");

            entity.HasIndex(e => new { e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }, "fk_Segmento_Cliente1_idx");

            entity.Property(e => e.IdSegmento)
                .ValueGeneratedOnAdd()
                .HasColumnName("idSegmento");
            entity.Property(e => e.ClienteIdCliente).HasColumnName("Cliente_idCliente");
            entity.Property(e => e.ClientePersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Cliente_Persona_Cedula");
            entity.Property(e => e.ClientePersonaTipoPersonaIdtipoPersona).HasColumnName("Cliente_Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.Criterio)
                .HasMaxLength(100)
                .HasColumnName("criterio");
            entity.Property(e => e.DescripcionSegmento)
                .HasMaxLength(255)
                .HasColumnName("descripcion_segmento");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Segmentos)
                .HasForeignKey(d => new { d.ClienteIdCliente, d.ClientePersonaCedula, d.ClientePersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("segmento$fk_Segmento_Cliente1");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.HasKey(e => new { e.IdTelefono, e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }).HasName("PK_telefono_idTelefono");

            entity.ToTable("telefono", "ecomconnect");

            entity.HasIndex(e => new { e.ClienteIdCliente, e.ClientePersonaCedula, e.ClientePersonaTipoPersonaIdtipoPersona }, "fk_Telefono_Cliente1_idx");

            entity.HasIndex(e => e.TipoTelefonoIdTipoTelefono, "fk_Telefono_Tipo_Telefono_idx");

            entity.Property(e => e.IdTelefono)
                .ValueGeneratedOnAdd()
                .HasColumnName("idTelefono");
            entity.Property(e => e.ClienteIdCliente).HasColumnName("Cliente_idCliente");
            entity.Property(e => e.ClientePersonaCedula)
                .HasMaxLength(10)
                .HasColumnName("Cliente_Persona_Cedula");
            entity.Property(e => e.ClientePersonaTipoPersonaIdtipoPersona).HasColumnName("Cliente_Persona_tipo_persona_idtipo_persona");
            entity.Property(e => e.TipoTelefonoIdTipoTelefono).HasColumnName("Tipo_Telefono_idTipo_Telefono");

            entity.HasOne(d => d.TipoTelefonoIdTipoTelefonoNavigation).WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.TipoTelefonoIdTipoTelefono)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("telefono$fk_Telefono_Tipo_Telefono");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Telefonos)
                .HasForeignKey(d => new { d.ClienteIdCliente, d.ClientePersonaCedula, d.ClientePersonaTipoPersonaIdtipoPersona })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("telefono$fk_Telefono_Cliente1");
        });

        modelBuilder.Entity<TipoCorreo>(entity =>
        {
            entity.HasKey(e => e.IdTipoCorreo).HasName("PK_tipo_correo_idTipo_Correo");

            entity.ToTable("tipo_correo", "ecomconnect");

            entity.Property(e => e.IdTipoCorreo).HasColumnName("idTipo_Correo");
            entity.Property(e => e.DescripcionCorreo)
                .HasMaxLength(45)
                .HasColumnName("Descripcion_correo");
        });

        modelBuilder.Entity<TipoDireccion>(entity =>
        {
            entity.HasKey(e => e.IdTipoDireccion).HasName("PK_tipo_direccion_idTipo_Direccion");

            entity.ToTable("tipo_direccion", "ecomconnect");

            entity.Property(e => e.IdTipoDireccion).HasColumnName("idTipo_Direccion");
            entity.Property(e => e.DescripcionTipoDireccion)
                .HasMaxLength(45)
                .HasColumnName("Descripcion_TipoDireccion");
        });

        modelBuilder.Entity<TipoPersona>(entity =>
        {
            entity.HasKey(e => e.IdtipoPersona).HasName("PK_tipo_persona_idtipo_persona");

            entity.ToTable("tipo_persona", "ecomconnect");

            entity.Property(e => e.IdtipoPersona).HasColumnName("idtipo_persona");
            entity.Property(e => e.DescripcionTipopersona)
                .HasMaxLength(150)
                .HasColumnName("descripcion_tipopersona");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoTelefono>(entity =>
        {
            entity.HasKey(e => e.IdTipoTelefono).HasName("PK_tipo_telefono_idTipo_Telefono");

            entity.ToTable("tipo_telefono", "ecomconnect");

            entity.Property(e => e.IdTipoTelefono).HasColumnName("idTipo_Telefono");
            entity.Property(e => e.DescripcionTipotelefono)
                .HasMaxLength(45)
                .HasColumnName("descripcion_tipotelefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
