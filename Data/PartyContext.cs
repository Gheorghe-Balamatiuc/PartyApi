using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PartyApi.Models;

namespace PartyApi.Data;

public partial class PartyContext : DbContext
{
    public PartyContext()
    {
    }

    public PartyContext(DbContextOptions<PartyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MemberResponsibility> MemberResponsibilities { get; set; }

    public virtual DbSet<Party> Parties { get; set; }

    public virtual DbSet<PartyMember> PartyMembers { get; set; }

    public virtual DbSet<Responsibility> Responsibilities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MemberResponsibility>(entity =>
        {
            entity.HasKey(e => e.MemberResponsibilityId).HasName("PK__member_r__8026347556FF1725");

            entity.ToTable("member_responsibility");

            entity.Property(e => e.MemberResponsibilityId).HasColumnName("member_responsibility_id");
            entity.Property(e => e.PartyId).HasColumnName("party_id");
            entity.Property(e => e.PartyMemberId).HasColumnName("party_member_id");
            entity.Property(e => e.ResponsibilityId).HasColumnName("responsibility_id");

            entity.HasOne(d => d.Party).WithMany(p => p.MemberResponsibilities)
                .HasForeignKey(d => d.PartyId)
                .HasConstraintName("FK__member_re__party__3D5E1FD2");

            entity.HasOne(d => d.PartyMember).WithMany(p => p.MemberResponsibilities)
                .HasForeignKey(d => d.PartyMemberId)
                .HasConstraintName("FK__member_re__party__3E52440B");

            entity.HasOne(d => d.Responsibility).WithMany(p => p.MemberResponsibilities)
                .HasForeignKey(d => d.ResponsibilityId)
                .HasConstraintName("FK__member_re__respo__3F466844");
        });

        modelBuilder.Entity<Party>(entity =>
        {
            entity.HasKey(e => e.PartyId).HasName("PK__party__8A2AF38E47E8E627");

            entity.ToTable("party");

            entity.Property(e => e.PartyId).HasColumnName("party_id");
            entity.Property(e => e.Budget)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("budget");
            entity.Property(e => e.PartyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("party_name");
        });

        modelBuilder.Entity<PartyMember>(entity =>
        {
            entity.HasKey(e => e.PartyMemberId).HasName("PK__party_me__127CD03BD4876658");

            entity.ToTable("party_member");

            entity.Property(e => e.PartyMemberId).HasColumnName("party_member_id");
            entity.Property(e => e.PartyMemberName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("party_member_name");
            entity.Property(e => e.PartyMemberSurname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("party_member_surname");
        });

        modelBuilder.Entity<Responsibility>(entity =>
        {
            entity.HasKey(e => e.ResponsibilityId).HasName("PK__responsi__0FD26EB952A7DA2B");

            entity.ToTable("responsibility");

            entity.Property(e => e.ResponsibilityId).HasColumnName("responsibility_id");
            entity.Property(e => e.ResponsibilityName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("responsibility_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
