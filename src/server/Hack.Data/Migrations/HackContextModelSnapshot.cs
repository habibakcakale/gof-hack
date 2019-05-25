﻿// <auto-generated />
using Hack.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hack.Data.Migrations
{
    [DbContext(typeof(HackContext))]
    partial class HackContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hack.Domain.Epic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Epic");
                });

            modelBuilder.Entity("Hack.Domain.EpicRequirement", b =>
                {
                    b.Property<int>("EpicId");

                    b.Property<int>("RequirementId");

                    b.HasKey("EpicId", "RequirementId");

                    b.HasIndex("RequirementId");

                    b.ToTable("EpicRequirement");
                });

            modelBuilder.Entity("Hack.Domain.Phase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Phase");
                });

            modelBuilder.Entity("Hack.Domain.PhaseSection", b =>
                {
                    b.Property<int>("PhaseId");

                    b.Property<int>("SectionId");

                    b.HasKey("PhaseId", "SectionId");

                    b.HasIndex("SectionId");

                    b.ToTable("PhaseSection");
                });

            modelBuilder.Entity("Hack.Domain.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Hack.Domain.ProjectPhase", b =>
                {
                    b.Property<int>("PhaseId");

                    b.Property<int>("ProjectId");

                    b.HasKey("PhaseId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectPhase");
                });

            modelBuilder.Entity("Hack.Domain.Requirement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("JiraTicketId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Requirement");
                });

            modelBuilder.Entity("Hack.Domain.RequirementEstimation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Actual");

                    b.Property<decimal>("Estimated");

                    b.HasKey("Id");

                    b.ToTable("RequirementEstimation");
                });

            modelBuilder.Entity("Hack.Domain.RequirementRequirementEstimation", b =>
                {
                    b.Property<int>("RequirementId");

                    b.Property<int>("RequirementEstimationId");

                    b.HasKey("RequirementId", "RequirementEstimationId");

                    b.HasIndex("RequirementEstimationId");

                    b.ToTable("RequirementRequirementEstimation");
                });

            modelBuilder.Entity("Hack.Domain.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("Hack.Domain.SectionEpic", b =>
                {
                    b.Property<int>("EpicId");

                    b.Property<int>("SectionId");

                    b.HasKey("EpicId", "SectionId");

                    b.HasIndex("SectionId");

                    b.ToTable("SectionEpic");
                });

            modelBuilder.Entity("Hack.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("PasswordHashed")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("Username");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Hack.Domain.EpicRequirement", b =>
                {
                    b.HasOne("Hack.Domain.Epic", "Epic")
                        .WithMany("EpicRequirements")
                        .HasForeignKey("EpicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hack.Domain.Requirement", "Requirement")
                        .WithMany("EpicRequirements")
                        .HasForeignKey("RequirementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hack.Domain.PhaseSection", b =>
                {
                    b.HasOne("Hack.Domain.Phase", "Phase")
                        .WithMany("PhaseSections")
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hack.Domain.Section", "Section")
                        .WithMany("PhaseSections")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hack.Domain.Project", b =>
                {
                    b.HasOne("Hack.Domain.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hack.Domain.ProjectPhase", b =>
                {
                    b.HasOne("Hack.Domain.Phase", "Phase")
                        .WithMany("ProjectPhases")
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hack.Domain.Project", "Project")
                        .WithMany("ProjectPhases")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hack.Domain.RequirementRequirementEstimation", b =>
                {
                    b.HasOne("Hack.Domain.RequirementEstimation", "RequirementEstimation")
                        .WithMany("RequirementRequirementEstimations")
                        .HasForeignKey("RequirementEstimationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hack.Domain.Requirement", "Requirement")
                        .WithMany("RequirementRequirementEstimations")
                        .HasForeignKey("RequirementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hack.Domain.SectionEpic", b =>
                {
                    b.HasOne("Hack.Domain.Epic", "Epic")
                        .WithMany("SectionEpics")
                        .HasForeignKey("EpicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hack.Domain.Section", "Section")
                        .WithMany("SectionEpics")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
